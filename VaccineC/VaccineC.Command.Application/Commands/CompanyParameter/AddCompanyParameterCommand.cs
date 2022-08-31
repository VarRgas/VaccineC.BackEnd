using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanyParameter
{
    public class AddCompanyParameterCommand : IRequest<CompaniesParametersViewModel>
    {
        public Guid ID;
        public Guid CompanyId;
        public int ApplicationTimePerMinute;
        public int MaximumDaysBudgetValidity;
        public DateTime Register;
        public string ScheduleColor;

        public AddCompanyParameterCommand(Guid id, Guid companyId, int applicationTimePerMinute, int maximumDaysBudgetValidity, DateTime register, string scheduleColor)
        {
            ID = id;
            CompanyId = companyId;
            ApplicationTimePerMinute = applicationTimePerMinute;
            MaximumDaysBudgetValidity = maximumDaysBudgetValidity;
            Register = register;
            ScheduleColor = scheduleColor;
        }
    }
}
