using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceListQuery : IRequest<IEnumerable<ResourceViewModel>>
    {

    }
}
