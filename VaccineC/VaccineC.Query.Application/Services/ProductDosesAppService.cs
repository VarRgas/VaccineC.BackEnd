using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class ProductDosesAppService : IProductDosesAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public ProductDosesAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> GetAllAsync()
        {
            var productsDoses = await _queryContext.AllProductsDoses.ToListAsync();
            var productsDosesViewModel = productsDoses.Select(r => _mapper.Map<ProductDosesViewModel>(r)).ToList();
            return productsDosesViewModel;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> GetProductsDosesByProductId(Guid productsId)
        {

            var productsDoses = await _queryContext.AllProductsDoses.ToListAsync();
            var productsDosesViewModel = productsDoses
                .Select(r => _mapper.Map<ProductDosesViewModel>(r))
                .Where(r => r.ProductsId == productsId)
                .ToList();
            return productsDosesViewModel;

        }

        public ProductDosesViewModel GetById(Guid id)
        {
            var productDoses = _mapper.Map<ProductDosesViewModel>(_queryContext.AllProductsDoses
                                 .Where(r => r.ID == id)
                                 .First());
            return productDoses;
        }
    }
}
