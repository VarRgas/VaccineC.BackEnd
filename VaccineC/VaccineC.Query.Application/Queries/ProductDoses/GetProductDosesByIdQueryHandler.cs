using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductDosesByIdQueryHandler : IRequestHandler<GetProductDosesByIdQuery, ProductDosesViewModel>
    {

        private readonly IMediator _mediator;

        public GetProductDosesByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductDosesViewModel> Handle(GetProductDosesByIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetProductDosesListQuery());
            var product = products.FirstOrDefault(pf => pf.ID == request.Id);
            return product;

        }
    }
}
