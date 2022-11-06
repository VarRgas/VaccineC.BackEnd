using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Company
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, CompanyViewModel>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository, IQueryContext queryContext, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<CompanyViewModel> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {

            var updatedCompany = _companyRepository.GetById(request.ID);

            if (updatedCompany == null)
            {
                throw new ArgumentException("Empresa não encontrado!");
            }

            updatedCompany.SetPersonId(request.PersonId);
            updatedCompany.SetDetails(request.Details);
            updatedCompany.SetRegister(DateTime.Now);

            await _companyRepository.SaveChangesAsync();

            var companies = await _queryContext.AllCompanies.Where(c => c.ID == updatedCompany.ID).ToListAsync();
            var company = companies.Select(r => _mapper.Map<CompanyViewModel>(r)).FirstOrDefault();

            return company;

        }
    }
}
