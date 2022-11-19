using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationSipniIntegrationByParameterQuery : IRequest<IEnumerable<ApplicationSipniViewModel>>
    {
        public string Borrower;
        public string Situation;

        public GetApplicationSipniIntegrationByParameterQuery(string borrower, string situation)
        {
            Borrower = borrower;
            Situation = situation;
        }
    }
}
