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
    public class AddPhysicalComplementsCommandHandler : IRequestHandler<AddPhysicalComplementsCommand, PersonsPhysicalViewModel>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly VaccineCContext _context;
        private readonly IMapper _mapper;

        public AddPhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository, IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }
        public async Task<PersonsPhysicalViewModel> Handle(AddPhysicalComplementsCommand request, CancellationToken cancellationToken)
        {

            await isExistingCpf(request.CpfNumber);

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

        public async Task<Boolean> isExistingCpf(String cpf)
        {

            var person = (from pp in _context.PersonsPhysical
                          join p in _context.Persons on pp.PersonID equals p.ID
                          where pp.CpfNumber.Equals(cpf)
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
