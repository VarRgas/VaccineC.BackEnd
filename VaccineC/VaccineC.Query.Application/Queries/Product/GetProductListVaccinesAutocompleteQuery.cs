using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductListVaccinesAutocompleteQuery : IRequest<IEnumerable<SbimVaccinesViewModel>>
    {
    }
}
