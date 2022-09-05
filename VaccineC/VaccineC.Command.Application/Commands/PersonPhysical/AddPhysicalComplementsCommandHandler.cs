using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class AddPhysicalComplementsCommandHandler : IRequestHandler<AddPhysicalComplementsCommand, PersonsPhysicalViewModel>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;

        public AddPhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
        }
        public async Task<PersonsPhysicalViewModel> Handle(AddPhysicalComplementsCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.PersonsPhysical newPersonsPhysical = new Domain.Entities.PersonsPhysical(
                Guid.NewGuid(),
                request.PersonID,
                request.MaritalStatus,
                request.Gender,
                request.DeathDate,
                DateTime.Now,
                request.CnsNumber,
                request.CpfNumber
            );

            _repository.Add(newPersonsPhysical);
            await _repository.SaveChangesAsync();

            return new PersonsPhysicalViewModel()
            {
                ID = newPersonsPhysical.ID,
                PersonID = newPersonsPhysical.PersonId,
                MaritalStatus = newPersonsPhysical.MaritalStatus,
                Gender = newPersonsPhysical.Gender,
                DeathDate = newPersonsPhysical.DeathDate,
                Register = newPersonsPhysical.Register,
                CnsNumber = newPersonsPhysical.CnsNumber,
                CpfNumber = newPersonsPhysical.CpfNumber,
            };

        }
    }
}
