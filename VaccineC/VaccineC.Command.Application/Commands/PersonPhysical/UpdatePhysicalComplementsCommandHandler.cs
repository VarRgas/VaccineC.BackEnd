using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.PersonPhysical
{
    public class UpdatePhysicalComplementsCommandHandler : IRequestHandler<UpdatePhysicalComplementsCommand, PersonsPhysicalViewModel>
    {
        private readonly IPersonPhysicalAppService _personPhysicalAppService;
        private readonly IPersonPhysicalRepository _repository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UpdatePhysicalComplementsCommandHandler(IPersonPhysicalAppService personPhysicalAppService, IPersonPhysicalRepository repository, IQueryContext queryContext, IMapper mapper)
        {
            _personPhysicalAppService = personPhysicalAppService;
            _repository = repository;
            _queryContext = queryContext;
            _mapper = mapper;
        }
        public async Task <PersonsPhysicalViewModel> Handle(UpdatePhysicalComplementsCommand request, CancellationToken cancellationToken)
        {

            await isExistingCpf(request.CpfNumber, request.ID);

            var physicalComplements = _repository.GetById(request.ID);
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

            var personsPhysicals = await _queryContext.AllPersonsPhysicals.ToListAsync();
            var personPhysicalViewModel = personsPhysicals
                .Select(r => _mapper.Map<PersonsPhysicalViewModel>(r))
                .Where(r => r.CpfNumber.Equals(cpf) && r.ID != id)
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
