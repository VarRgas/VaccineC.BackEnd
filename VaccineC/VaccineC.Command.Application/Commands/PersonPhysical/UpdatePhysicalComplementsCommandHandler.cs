using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class UpdatePhysicalComplementsCommandHandler : IRequestHandler<UpdatePhysicalComplementsCommand, PersonsPhysicalViewModel>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;

        public UpdatePhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
        }
        public async Task <PersonsPhysicalViewModel> Handle(UpdatePhysicalComplementsCommand request, CancellationToken cancellationToken)
        {

            var physicalComplements = _repository.GetById(request.ID);
            physicalComplements.SetMaritalStatus(request.MaritalStatus);
            physicalComplements.SetCPF(request.CpfNumber);
            physicalComplements.SetCNS(request.CnsNumber);
            physicalComplements.SetGender(request.Gender);
            physicalComplements.SetDeathDate(request.DeathDate);
            physicalComplements.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            return new PersonsPhysicalViewModel()
            {
                ID = physicalComplements.ID,
                PersonID = physicalComplements.PersonId,
                MaritalStatus = physicalComplements.MaritalStatus,
                Gender = physicalComplements.Gender,
                DeathDate = physicalComplements.DeathDate,
                Register = physicalComplements.Register,
                CnsNumber = physicalComplements.CnsNumber,
                CpfNumber = physicalComplements.CpfNumber,
            };
        }
    }
}
