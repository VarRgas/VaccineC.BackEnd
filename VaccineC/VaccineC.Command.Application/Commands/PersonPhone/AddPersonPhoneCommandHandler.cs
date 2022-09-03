using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhone
{
    public class AddPersonPhoneCommandHandler : IRequestHandler<AddPersonPhoneCommand, IEnumerable<PersonPhoneViewModel>>
    {
        private readonly IPersonPhoneRepository _personPhoneRepository;
        private readonly IPersonPhoneAppService _personPhoneAppService;
        private readonly VaccineCCommandContext _ctx;

        public AddPersonPhoneCommandHandler(IPersonPhoneRepository personPhoneRepository, IPersonPhoneAppService personPhoneAppService, VaccineCCommandContext ctx)
        {
            _personPhoneRepository = personPhoneRepository;
            _personPhoneAppService = personPhoneAppService;
            _ctx = ctx;
        }
        public async Task<IEnumerable<PersonPhoneViewModel>> Handle(AddPersonPhoneCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.PersonPhone newPersonPhone = new Domain.Entities.PersonPhone(
                Guid.NewGuid(),
                request.PersonID,
                request.PhoneType,
                request.NumberPhone,
                request.CodeArea,
                DateTime.Now
            );

            _personPhoneRepository.Add(newPersonPhone);
            await _personPhoneRepository.SaveChangesAsync();
            return await _personPhoneAppService.GetAllPersonsPhonesByPersonId(newPersonPhone.PersonID);

        }
    }
}
