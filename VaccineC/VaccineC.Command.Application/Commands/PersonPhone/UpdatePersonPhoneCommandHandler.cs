using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhone
{
    public class UpdatePersonPhoneCommandHandler : IRequestHandler<UpdatePersonPhoneCommand, IEnumerable<PersonPhoneViewModel>>
    {
        private readonly IPersonPhoneRepository _personPhoneRepository;
        private readonly IPersonPhoneAppService _personPhoneAppService;

        public UpdatePersonPhoneCommandHandler(IPersonPhoneRepository personPhoneRepository, IPersonPhoneAppService personPhoneAppService)
        {
            _personPhoneRepository = personPhoneRepository;
            _personPhoneAppService = personPhoneAppService;
        }

        public async Task<IEnumerable<PersonPhoneViewModel>> Handle(UpdatePersonPhoneCommand request, CancellationToken cancellationToken)
        {

            var personPhone = _personPhoneRepository.GetById(request.ID);

            if (personPhone == null)
            {
                throw new ArgumentException("Telefone não encontrado!");
            }

            personPhone.SetNumberPhone(request.NumberPhone);
            personPhone.SetCodeArea(request.CodeArea);
            personPhone.SetPhoneType(request.PhoneType);
            personPhone.SetRegister(DateTime.Now);

            await _personPhoneRepository.SaveChangesAsync();

            return await _personPhoneAppService.GetAllPersonsPhonesByPersonId(personPhone.PersonID);
        }
    }
}
