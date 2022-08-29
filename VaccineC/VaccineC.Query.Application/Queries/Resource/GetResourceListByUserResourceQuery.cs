using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceListByUserResourceQuery : IRequest<IEnumerable<ResourceViewModel>>
    {
        public Guid UserId { get; set; }

        public GetResourceListByUserResourceQuery(Guid userId)
        {
            UserId = userId;
        }

    }
}
