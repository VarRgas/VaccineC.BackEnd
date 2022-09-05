using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonJuridical
{
    public class AddJuridicalComplementsCommandHandler : IRequestHandler<AddJuridicalComplementsCommand, IEnumerable<PersonsJuridicalViewModel>>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;
        private readonly IPersonJuridicalRepository _repository;

        public AddJuridicalComplementsCommandHandler(IPersonJuridicalAppService personJuridicalAppService, IPersonJuridicalRepository repository)
        {
            _personJuridicalAppService = personJuridicalAppService;
            _repository = repository;
        }
        public async Task<IEnumerable<PersonsJuridicalViewModel>> Handle(AddJuridicalComplementsCommand request, CancellationToken cancellationToken)
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
            return await _personJuridicalAppService.GetAllJuridicalComplementsByPersonId(newPersonsJuridical.ID);

        }
    }
}
