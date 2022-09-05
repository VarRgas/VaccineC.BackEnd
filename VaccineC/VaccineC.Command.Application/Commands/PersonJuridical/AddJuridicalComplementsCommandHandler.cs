using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonJuridical
{
    public class AddJuridicalComplementsCommandHandler : IRequestHandler<AddJuridicalComplementsCommand, PersonsJuridicalViewModel>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;
        private readonly IPersonJuridicalRepository _repository;

        public AddJuridicalComplementsCommandHandler(IPersonJuridicalAppService personJuridicalAppService, IPersonJuridicalRepository repository)
        {
            _personJuridicalAppService = personJuridicalAppService;
            _repository = repository;
        }
        public async Task<PersonsJuridicalViewModel> Handle(AddJuridicalComplementsCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.PersonsJuridical newPersonsJuridical = new Domain.Entities.PersonsJuridical(
                Guid.NewGuid(),
                request.PersonID,
                request.FantasyName,
                request.CnpjNumber,
                DateTime.Now
            );

            _repository.Add(newPersonsJuridical);
            await _repository.SaveChangesAsync();

            return new PersonsJuridicalViewModel()
            {
                ID = newPersonsJuridical.ID,
                PersonID = newPersonsJuridical.PersonId,
                FantasyName = newPersonsJuridical.FantasyName,
                CnpjNumber = newPersonsJuridical.CnpjNumber,
                Register = newPersonsJuridical.Register,
            };
        }
    }
}
