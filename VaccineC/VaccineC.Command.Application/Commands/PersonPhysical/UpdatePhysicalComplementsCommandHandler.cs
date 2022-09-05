using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class UpdatePhysicalComplementsCommandHandler : IRequestHandler<AddPhysicalComplementsCommand, IEnumerable<PersonsPhysicalViewModel>>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;

        public UpdatePhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
        }
        public async Task<IEnumerable<PersonsPhysicalViewModel>> Handle(AddPhysicalComplementsCommand request, CancellationToken cancellationToken)
        {

            var personPhone = _repository.GetById(request.ID);
            personPhone.SetMaritalStatus(request.MaritalStatus);
            personPhone.SetGender(request.Gender);
            personPhone.SetDeathDate(request.DeathDate);
            personPhone.SetRegister(DateTime.Now);
            personPhone.SetCNS(request.CpfNumber);
            personPhone.SetCPF(request.CnsNumber);

            await _repository.SaveChangesAsync();

            return await _personPhysicalAppService.GetAllPhysicalComplementsByPersonId(personPhone.ID);

        }
    }
}
