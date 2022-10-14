using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Authorization
{
    public class SuggestJuridicalDosesCommand : IRequest<IEnumerable<BudgetProductViewModel>>
    {
        public List<AuthorizationSuggestionViewModel> ListAuthorizationSuggestionViewModel;

        public SuggestJuridicalDosesCommand(List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {
            ListAuthorizationSuggestionViewModel = listAuthorizationSuggestionViewModel;
        }
    }
}
