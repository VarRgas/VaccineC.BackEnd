using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductsDosesByTypeQueryHandler : IRequestHandler<GetProductsDosesByTypeQuery, IEnumerable<ProductDosesViewModel>>
    {
        private readonly IProductDosesAppService _appService;

        public GetProductsDosesByTypeQueryHandler(IProductDosesAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> Handle(GetProductsDosesByTypeQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetByType(request.DoseType);
        }

    }
}
