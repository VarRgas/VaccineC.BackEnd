using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Application
{
    public class GetPersonApplicationProductSameDayQuery : IRequest<Boolean>
    {
        public Guid PersonId;
        public Guid ProductId;

        public GetPersonApplicationProductSameDayQuery(Guid personId, Guid productId)
        {
            PersonId = personId;
            ProductId = productId;
        }
    }
}
