using MediatR;
using VaccineC.Query.Application.ViewModels;
namespace VaccineC.Command.Application.Commands.CompanySchedule
{
    public class AddCompanyScheduleOnDemandCommand : IRequest<IEnumerable<CompanyScheduleViewModel>>
    {

        public List<CompanyScheduleViewModel> ListCompanyScheduleViewModel;

        public AddCompanyScheduleOnDemandCommand(List<CompanyScheduleViewModel> listCompanyScheduleViewModel)
        {
            ListCompanyScheduleViewModel = listCompanyScheduleViewModel;
        }

    }
}
