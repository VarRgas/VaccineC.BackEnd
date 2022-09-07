using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.PersonJuridical
{
    public class AddJuridicalComplementsCommandHandler : IRequestHandler<AddJuridicalComplementsCommand, PersonsJuridicalViewModel>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;
        private readonly IPersonJuridicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public AddJuridicalComplementsCommandHandler(IPersonJuridicalAppService personJuridicalAppService, IPersonJuridicalRepository repository, IQueryContext queryContext, IMapper mapper)
        {
            _personJuridicalAppService = personJuridicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
        }
        public async Task<PersonsJuridicalViewModel> Handle(AddJuridicalComplementsCommand request, CancellationToken cancellationToken)
        {
            await isExistingCnpj(request.CnpjNumber);

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

        public async Task<Boolean> isExistingCnpj(String cnpj)
        {

            var personsJuridicals = await _queryContext.AllPersonsJuridicals.ToListAsync();
            var personJuridicalViewModel = personsJuridicals
                .Select(r => _mapper.Map<PersonsJuridicalViewModel>(r))
                .Where(r => r.CnpjNumber.Equals(cnpj))
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
