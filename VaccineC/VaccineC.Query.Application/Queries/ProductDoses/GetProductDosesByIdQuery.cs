using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductDosesByIdQuery : IRequest<ProductDosesViewModel>
    {
        public Guid Id;

        public GetProductDosesByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
