using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IEnumerable<ProductViewModel>>
    {

        private readonly IProductAppService _appService;

        public GetProductListQueryHandler(IProductAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ProductViewModel>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllAsync();
        }
    }
}
