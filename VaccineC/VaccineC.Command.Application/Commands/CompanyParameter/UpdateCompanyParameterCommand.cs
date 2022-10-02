using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanyParameter
{
    public class UpdateCompanyParameterCommand : IRequest<CompaniesParametersViewModel>
    {
        public Guid ID;
        public Guid CompanyId;
        public Guid? DefaultPaymentFormId;
        public int ApplicationTimePerMinute;
        public int MaximumDaysBudgetValidity;
        public DateTime Register;
        public string ScheduleColor;

        public UpdateCompanyParameterCommand(Guid id, Guid companyId, Guid? defaultPaymentFormId, int applicationTimePerMinute, int maximumDaysBudgetValidity, DateTime register, string scheduleColor)
        {
            ID = id;
            CompanyId = companyId;
            DefaultPaymentFormId = defaultPaymentFormId;
            ApplicationTimePerMinute = applicationTimePerMinute;
            MaximumDaysBudgetValidity = maximumDaysBudgetValidity;
            Register = register;
            ScheduleColor = scheduleColor;
        }

    }
}
