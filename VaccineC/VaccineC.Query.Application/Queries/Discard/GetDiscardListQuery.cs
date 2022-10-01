using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Discard
{
    public class GetDiscardListQuery : IRequest<IEnumerable<DiscardViewModel>>
    {
    }
}
