using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanyParameter
{
    public class UpdateCompanyParameterCommandHandler : IRequestHandler<UpdateCompanyParameterCommand, CompaniesParametersViewModel>
    {
        private readonly ICompanyParameterRepository _companyParameterRepository;

        public UpdateCompanyParameterCommandHandler(ICompanyParameterRepository companyParameterRepository)
        {
            _companyParameterRepository = companyParameterRepository;
        }

        public async Task<CompaniesParametersViewModel> Handle(UpdateCompanyParameterCommand request, CancellationToken cancellationToken)
        {

            var companyParameter = _companyParameterRepository.GetById(request.ID);
            companyParameter.SetMaximumDaysBudgetValidity(request.MaximumDaysBudgetValidity);
            companyParameter.SetApplicationTimePerMinute(request.ApplicationTimePerMinute);
            companyParameter.SetScheduleColor(request.ScheduleColor);
            companyParameter.SetRegister(DateTime.Now);

            await _companyParameterRepository.SaveChangesAsync();

            return new CompaniesParametersViewModel()
            {
                ID = companyParameter.ID,
                CompanyId = companyParameter.CompanyId,
                ApplicationTimePerMinute = companyParameter.ApplicationTimePerMinute,
                MaximumDaysBudgetValidity = companyParameter.MaximumDaysBudgetValidity,
                ScheduleColor = companyParameter.ScheduleColor
            };

        }
    }
}
