using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductDosesListQueryHandler : IRequestHandler<GetProductDosesListQuery, IEnumerable<ProductDosesViewModel>>
    {

        private readonly IProductDosesAppService _appService;

        public GetProductDosesListQueryHandler(IProductDosesAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> Handle(GetProductDosesListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
