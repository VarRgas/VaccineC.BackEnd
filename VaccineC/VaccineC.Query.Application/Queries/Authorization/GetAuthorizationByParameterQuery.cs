using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByParameterQuery : IRequest<IEnumerable<AuthorizationViewModel>>
    {
        public string Information;

        public GetAuthorizationByParameterQuery(string information)
        {
            Information = information;
        }
    }
}
