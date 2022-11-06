using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhone
{
    public class DeletePersonPhoneCommandHandler : IRequestHandler<DeletePersonPhoneCommand, IEnumerable<PersonPhoneViewModel>>
    {
        private readonly IPersonPhoneRepository _personPhoneRepository;
        private readonly IPersonPhoneAppService _personPhoneAppService;
        private readonly VaccineCCommandContext _ctx;

        public DeletePersonPhoneCommandHandler(IPersonPhoneRepository personPhoneRepository, IPersonPhoneAppService personPhoneAppService, VaccineCCommandContext ctx)
        {
            _personPhoneRepository = personPhoneRepository;
            _personPhoneAppService = personPhoneAppService;
            _ctx = ctx;
        }

        public async Task<IEnumerable<PersonPhoneViewModel>> Handle(DeletePersonPhoneCommand request, CancellationToken cancellationToken)
        {

            var personPhone = _personPhoneRepository.GetById(request.Id);

            if (personPhone == null)
            {
                throw new ArgumentException("Telefone não encontrado!");
            }

            _personPhoneRepository.Remove(personPhone);
            await _personPhoneRepository.SaveChangesAsync();

            return await _personPhoneAppService.GetAllPersonsPhonesByPersonId(personPhone.PersonID);

        }
    }
}
