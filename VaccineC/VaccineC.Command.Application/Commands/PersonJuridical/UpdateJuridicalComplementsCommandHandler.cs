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
    public class UpdateJuridicalComplementsCommandHandler : IRequestHandler<UpdateJuridicalComplementsCommand, PersonsJuridicalViewModel>
    {
        private readonly IPersonJuridicalAppService _personJuridicalAppService;
        private readonly IPersonJuridicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public UpdateJuridicalComplementsCommandHandler(IPersonJuridicalAppService personJuridicalAppService, IPersonJuridicalRepository repository, IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _personJuridicalAppService = personJuridicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
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
