using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Company
{
    public class AddCompanyCommandHandler : IRequestHandler<AddCompanyCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;

        public AddCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Guid> Handle(AddCompanyCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Company newCompany = new Domain.Entities.Company(Guid.NewGuid(),
                                                                              request.PersonID,
                                                                              request.Details,
                                                                              DateTime.Now);

            _companyRepository.Add(newCompany);

            await _companyRepository.SaveChangesAsync();
            return newCompany.ID;
        }

    }
}
