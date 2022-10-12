using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Command.Application.Commands.Authorization
{
    public class SuggestDosesCommand : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public List<AuthorizationSuggestionViewModel> ListAuthorizationSuggestionViewModel;

        public SuggestDosesCommand(List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {
            ListAuthorizationSuggestionViewModel = listAuthorizationSuggestionViewModel;
        }
    }
}
