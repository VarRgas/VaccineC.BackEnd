using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceByIdQuery : IRequest<ResourceViewModel>
    {
        public Guid Id;

        public GetResourceByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
