using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductListVaccinesAutocompleteQueryHandler : IRequestHandler<GetProductListVaccinesAutocompleteQuery, IEnumerable<ProductViewModel>>
    {
        private readonly IProductAppService _appService;

        public GetProductListVaccinesAutocompleteQueryHandler(IProductAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ProductViewModel>> Handle(GetProductListVaccinesAutocompleteQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetAllProductsVaccinesAutocomplete();
        }
    }
}
