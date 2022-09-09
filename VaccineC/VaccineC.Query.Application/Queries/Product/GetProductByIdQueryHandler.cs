using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductViewModel>
    {

        private readonly IMediator _mediator;

        public GetProductByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ProductViewModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetProductListQuery());
            var product = products.FirstOrDefault(pf => pf.ID == request.Id);
            return product;

        }
    }
}
