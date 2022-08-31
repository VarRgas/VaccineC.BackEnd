using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.CompanySchedule
{
    public class DeleteCompanyScheduleCommand : IRequest<IEnumerable<CompanyScheduleViewModel>>
    {
        public Guid Id;
        public DeleteCompanyScheduleCommand(Guid id)
        {
            Id = id;
        }
    }
}
