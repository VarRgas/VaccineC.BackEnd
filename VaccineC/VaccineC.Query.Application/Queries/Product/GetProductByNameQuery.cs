using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductByNameQuery : IRequest<IEnumerable<ProductViewModel>>
    {
        public string Name { get; set; }

        public GetProductByNameQuery(string name)
        {
            Name = name;
        }
    }
}
