using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.PersonJuridical
{
    public class UpdateJuridicalComplementsCommandHandler : IRequestHandler<UpdateJuridicalComplementsCommand, PersonsJuridicalViewModel>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;
        private readonly IPersonJuridicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UpdateJuridicalComplementsCommandHandler(IPersonJuridicalAppService personJuridicalAppService, IPersonJuridicalRepository repository, IQueryContext queryContext, IMapper mapper)
        {
            _personJuridicalAppService = personJuridicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
        }
        public async Task<PersonsJuridicalViewModel> Handle(UpdateJuridicalComplementsCommand request, CancellationToken cancellationToken)
        {

            await isExistingCnpj(request.CnpjNumber, request.ID);

            var juridicalComplement = _repository.GetById(request.ID);
            juridicalComplement.SetFantasyName(request.FantasyName);
            juridicalComplement.SetCnpjNumber(request.CnpjNumber);
            juridicalComplement.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            return new PersonsJuridicalViewModel()
            {
                ID = juridicalComplement.ID,
                PersonID = juridicalComplement.PersonId,
                FantasyName = juridicalComplement.FantasyName,
                CnpjNumber = juridicalComplement.CnpjNumber,
                Register = juridicalComplement.Register,
            };
        }

        public async Task<Boolean> isExistingCnpj(String cnpj, Guid id)
        {

            var personsJuridicals = await _queryContext.AllPersonsJuridicals.ToListAsync();
            var personJuridicalViewModel = personsJuridicals
                .Select(r => _mapper.Map<PersonsJuridicalViewModel>(r))
                .Where(r => r.CnpjNumber.Equals(cnpj) && r.ID != id)
                .FirstOrDefault();

            if (personJuridicalViewModel == null)
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Este CNPJ já está cadastrado para outra pessoa!");
            }

        }
    }
}
