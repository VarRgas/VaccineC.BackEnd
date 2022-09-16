using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.PersonJuridical
{
    public class AddJuridicalComplementsCommandHandler : IRequestHandler<AddJuridicalComplementsCommand, PersonsJuridicalViewModel>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;
        private readonly IPersonJuridicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public AddJuridicalComplementsCommandHandler(IPersonJuridicalAppService personJuridicalAppService, IPersonJuridicalRepository repository, IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _personJuridicalAppService = personJuridicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
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

            var person = (from pj in _context.PersonsJuridical
                          join p in _context.Persons on pj.PersonID equals p.ID
                          where pj.CnpjNumber.Equals(cnpj)
                          select p).FirstOrDefault();

            if (person == null)
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Este CNPJ já está cadastrado para a pessoa " + person.Name + "!");
            }

        }
    }
}
