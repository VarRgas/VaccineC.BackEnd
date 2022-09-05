using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class UpdatePhysicalComplementsCommandHandler : IRequestHandler<UpdatePhysicalComplementsCommand, IEnumerable<PersonsPhysicalViewModel>>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;

        public UpdatePhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
        }
        public async Task<IEnumerable<PersonsPhysicalViewModel>> Handle(UpdatePhysicalComplementsCommand request, CancellationToken cancellationToken)
        {

            var physicalComplements = _repository.GetById(request.ID);
            physicalComplements.SetMaritalStatus(request.MaritalStatus);
            physicalComplements.SetGender(request.Gender);
            physicalComplements.SetDeathDate(request.DeathDate);
            physicalComplements.SetRegister(DateTime.Now);
            physicalComplements.SetCNS(request.CpfNumber);
            physicalComplements.SetCPF(request.CnsNumber);

            await _repository.SaveChangesAsync();

            return await _personPhysicalAppService.GetAllPhysicalComplementsByPersonId(physicalComplements.ID);

        }
    }
}
