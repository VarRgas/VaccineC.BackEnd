using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Resource
{
    public class GetResourceByNameQuery : IRequest<IEnumerable<ResourceViewModel>>
    {
        public string Name { get; set; }

        public GetResourceByNameQuery(string name)
        {
            Name = name;
        }
    }
}
