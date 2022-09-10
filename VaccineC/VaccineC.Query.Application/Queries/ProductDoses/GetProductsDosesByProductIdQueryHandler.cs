using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductsDosesByProductIdQueryHandler : IRequestHandler<GetProductsDosesByProductIdQuery, IEnumerable<ProductDosesViewModel>>
    {
        private readonly IProductDosesAppService _appService;

        public GetProductsDosesByProductIdQueryHandler(IProductDosesAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> Handle(GetProductsDosesByProductIdQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetProductsDosesByProductId(request.ProductsId);
        }

    }
}
