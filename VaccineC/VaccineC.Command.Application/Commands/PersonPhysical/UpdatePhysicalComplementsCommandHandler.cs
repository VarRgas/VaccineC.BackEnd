using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class UpdatePhysicalComplementsCommandHandler : IRequestHandler<UpdatePhysicalComplementsCommand, PersonsPhysicalViewModel>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public UpdatePhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository, IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }
        public async Task <PersonsPhysicalViewModel> Handle(UpdatePhysicalComplementsCommand request, CancellationToken cancellationToken)
        {

            await isExistingCpf(request.CpfNumber, request.ID);

            var physicalComplements = _repository.GetById(request.ID);

            if (physicalComplements == null)
            {
                throw new ArgumentException("Complemento Pessoa Física não encontrado!");
            }

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

        public async Task<Boolean> isExistingCpf(String cpf, Guid id)
        {


            var person = (from pp in _context.PersonsPhysical
                          join p in _context.Persons on pp.PersonID equals p.ID
                          where pp.CpfNumber.Equals(cpf) && pp.ID != id
                          select p).FirstOrDefault();


            if (person == null)
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Este CPF já está cadastrado para a pessoa " + person.Name + "!");
            }

        }
    }
}
