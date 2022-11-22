using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanyParameter
{
    public class AddCompanyParameterCommand : IRequest<CompaniesParametersViewModel>
    {
        public Guid ID;
        public Guid CompanyId;
        public Guid? DefaultPaymentFormId;
        public int ApplicationTimePerMinute;
        public int MaximumDaysBudgetValidity;
        public TimeSpan StartTime;
        public TimeSpan FinalTime;
        public DateTime Register;

        public AddCompanyParameterCommand(Guid id, Guid companyId, Guid? defaultPaymentFormId, int applicationTimePerMinute, int maximumDaysBudgetValidity, TimeSpan startTime, TimeSpan finalTime, DateTime register)
        {
            ID = id;
            CompanyId = companyId;
            DefaultPaymentFormId = defaultPaymentFormId;
            ApplicationTimePerMinute = applicationTimePerMinute;
            MaximumDaysBudgetValidity = maximumDaysBudgetValidity;
            StartTime = startTime;
            FinalTime = finalTime;
            Register = register;
        }
    }
}
