using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Authorization
{
    public class GetAuthorizationByParameterQuery : IRequest<IEnumerable<AuthorizationViewModel>>
    {
        public string Information;
        public string Situation;
        public Guid ResponsibleId;

        public GetAuthorizationByParameterQuery(string information, string situation, Guid responsibleId)
        {
            Information = information;
            Situation = situation;
            ResponsibleId = responsibleId;
        }
    }
}
