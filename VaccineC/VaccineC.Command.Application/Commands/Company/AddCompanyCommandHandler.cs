using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Company
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, CompanyViewModel>
    {
        private readonly ICompanyRepository _companyRepository;

        public AddCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
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

            return new CompanyViewModel()
            {
                ID = newCompany.ID,
                PersonId = newCompany.PersonId,
                Details = newCompany.Details,
            };
        }

    }
}
