using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetHistoryApplicationsByPersonIdQuery : IRequest<IEnumerable<ApplicationHistoryViewModel>>
    {
        public Guid PersonId;

        public GetHistoryApplicationsByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
