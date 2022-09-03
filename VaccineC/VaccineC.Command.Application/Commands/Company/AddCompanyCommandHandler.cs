using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Company
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, CompanyViewModel>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public AddCompanyCommandHandler(ICompanyRepository companyRepository, IQueryContext queryContext, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<CompanyViewModel> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Company newCompany = new Domain.Entities.Company(
                Guid.NewGuid(),
                request.PersonID,
                request.Details,
                DateTime.Now
            );

            _companyRepository.Add(newCompany);
            await _companyRepository.SaveChangesAsync();

            var companies = await _queryContext.AllCompanies.Where(c => c.ID == newCompany.ID).ToListAsync();
            var company = companies.Select(r => _mapper.Map<CompanyViewModel>(r)).FirstOrDefault();

            return company;
        }

    }
}
