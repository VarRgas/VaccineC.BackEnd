using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class AddPhysicalComplementsCommandHandler : IRequestHandler<AddPhysicalComplementsCommand, PersonsPhysicalViewModel>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public AddPhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository, IQueryContext queryContext, IMapper mapper)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
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

            var personsPhysicals = await _queryContext.AllPersonsPhysicals.ToListAsync();
            var personPhysicalViewModel = personsPhysicals
                .Select(r => _mapper.Map<PersonsPhysicalViewModel>(r))
                .Where(r => r.CpfNumber.Equals(cpf))
                .FirstOrDefault();

            if (personPhysicalViewModel == null)
            {
                return false;
            }
            else
            {
                throw new ArgumentException("Este CPF já está cadastrado para outra pessoa!");
            }

        }

    }
}
