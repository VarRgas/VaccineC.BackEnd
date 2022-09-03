using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanySchedule
{
    public class UpdateCompanyScheduleCommand : IRequest<IEnumerable<CompanyScheduleViewModel>>
    {
        public Guid ID;
        public Guid CompanyId;
        public string Day;
        public TimeSpan StartTime;
        public TimeSpan FinalTime;
        public DateTime Register;

        public UpdateCompanyScheduleCommand(Guid id, Guid companyId, String day, TimeSpan startTime, TimeSpan finalTime, DateTime register)
        {
            ID = id;
            CompanyId = companyId;
            Day = day;
            StartTime = startTime;
            FinalTime = finalTime;
            Register = register;
        }
    }
}
