using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetAvailableApplicationsByPersonIdQuery : IRequest<IEnumerable<ApplicationAvailableViewModel>>
    {
        public Guid PersonId;

        public GetAvailableApplicationsByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
