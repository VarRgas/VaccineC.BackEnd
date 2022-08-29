using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.Company
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Guid>
    {
        private readonly ICompanyRepository _companyRepository;

        public UpdateCompanyCommandHandler(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<Guid> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {

            var user = _companyRepository.GetById(request.ID);
            user.SetPersonId(request.PersonId);
            user.SetDetails(request.Details);
            user.SetRegister(DateTime.Now);

            await _companyRepository.SaveChangesAsync();

            return user.ID;

        }
    }
}
