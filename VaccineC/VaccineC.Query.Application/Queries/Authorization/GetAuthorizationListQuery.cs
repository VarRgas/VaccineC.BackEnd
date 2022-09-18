using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationListQuery : IRequest<IEnumerable<AuthorizationViewModel>>
    {
    }
}
