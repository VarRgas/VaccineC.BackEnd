using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanyParameter
{
    public class AddCompanyParameterCommandHandler : IRequestHandler<AddCompanyParameterCommand, CompaniesParametersViewModel>
    {
        private readonly ICompanyParameterRepository _companyParameterRepository;

        public AddCompanyParameterCommandHandler(ICompanyParameterRepository companyParameterRepository)
        {
            _companyParameterRepository = companyParameterRepository;
        }

        public async Task<CompaniesParametersViewModel> Handle(AddCompanyParameterCommand request, CancellationToken cancellationToken)
        {

            Domain.Entities.CompanyParameter newCompanyParameter = new Domain.Entities.CompanyParameter(
                Guid.NewGuid(),
                request.CompanyId,
                request.DefaultPaymentFormId,
                request.ApplicationTimePerMinute,
                request.MaximumDaysBudgetValidity,
                request.StartTime,
                request.FinalTime,
                DateTime.Now
            );

            _companyParameterRepository.Add(newCompanyParameter);
            await _companyParameterRepository.SaveChangesAsync();

            return new CompaniesParametersViewModel()
            {
                ID = newCompanyParameter.ID,
                CompanyId = newCompanyParameter.CompanyId,
                DefaultPaymentFormId = newCompanyParameter.DefaultPaymentFormId,
                ApplicationTimePerMinute = newCompanyParameter.ApplicationTimePerMinute,
                MaximumDaysBudgetValidity = newCompanyParameter.MaximumDaysBudgetValidity,
                StartTime = newCompanyParameter.StartTime,
                FinalTime = newCompanyParameter.FinalTime
            };
        }
    }
}
