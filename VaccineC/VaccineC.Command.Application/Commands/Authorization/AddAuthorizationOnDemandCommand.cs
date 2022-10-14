using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class AddAuthorizationOnDemandCommand : IRequest
    {
        public List<AuthorizationViewModel> ListAuthorizationViewModel;

        public AddAuthorizationOnDemandCommand(List<AuthorizationViewModel> listAuthorizationViewModel)
        {
            ListAuthorizationViewModel = listAuthorizationViewModel;
        }
    }
}
