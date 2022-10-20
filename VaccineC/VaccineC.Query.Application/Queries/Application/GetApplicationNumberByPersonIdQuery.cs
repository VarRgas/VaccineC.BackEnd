using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetApplicationNumberByPersonIdQuery : IRequest<int>
    {
        public Guid PersonId;

        public GetApplicationNumberByPersonIdQuery(Guid personId)
        {
            PersonId = personId;
        }
    }
}
