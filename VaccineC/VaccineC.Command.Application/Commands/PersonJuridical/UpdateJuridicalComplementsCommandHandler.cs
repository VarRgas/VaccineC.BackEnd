using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.PersonJuridical
{
    public class UpdateJuridicalComplementsCommandHandler : IRequestHandler<AddJuridicalComplementsCommand, IEnumerable<PersonsJuridicalViewModel>>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;
        private readonly IPersonJuridicalRepository _repository;

        public UpdateJuridicalComplementsCommandHandler(IPersonJuridicalAppService personJuridicalAppService, IPersonJuridicalRepository repository)
        {
            _personJuridicalAppService = personJuridicalAppService;
            _repository = repository;
        }
        public async Task<IEnumerable<PersonsJuridicalViewModel>> Handle(AddJuridicalComplementsCommand request, CancellationToken cancellationToken)
        {

            var juridicalComplement = _repository.GetById(request.ID);
            juridicalComplement.SetFantasyName(request.FantasyName);
            juridicalComplement.SetCnpjNumber(request.CnpjNumber);
            juridicalComplement.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            return await _personJuridicalAppService.GetAllJuridicalComplementsByPersonId(juridicalComplement.ID);

        }
    }
}
