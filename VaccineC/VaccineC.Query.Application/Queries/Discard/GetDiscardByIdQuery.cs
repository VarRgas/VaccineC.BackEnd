using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Discard
{
    public class GetDiscardByIdQuery : IRequest<DiscardViewModel>
    {
        public Guid Id;

        public GetDiscardByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
