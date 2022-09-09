using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Product
{
    public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IEnumerable<ProductViewModel>>
    {
        private readonly IProductAppService _appService;

        public GetProductByNameQueryHandler(IProductAppService appService)
        {
            _appService = appService;
        }

        public async Task<IEnumerable<ProductViewModel>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            return await _appService.GetByName(request.Name);
        }

    }
}
