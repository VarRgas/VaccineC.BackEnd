using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Company
{
    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
    {
        private readonly ICompanyRepository _companyRepository;

        public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = _companyRepository.GetById(request.Id);

            if (company == null)
            {
                throw new ArgumentException("Empresa não encontrado!");
            }

            _companyRepository.Remove(company);
            await _companyRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
