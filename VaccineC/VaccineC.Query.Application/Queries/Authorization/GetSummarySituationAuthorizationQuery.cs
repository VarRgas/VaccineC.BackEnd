using MediatR;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetSummarySituationAuthorizationQuery : IRequest<IEnumerable<AuthorizationSummarySituationViewModel>>
    {
    }
}
