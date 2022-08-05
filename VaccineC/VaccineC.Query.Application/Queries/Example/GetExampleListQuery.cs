using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Example
{
    public class GetExampleListQuery : IRequest<IEnumerable<ExampleViewModel>>
    {
    }
}
