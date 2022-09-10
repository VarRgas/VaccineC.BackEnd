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
        private readonly VaccineCContext _context;

        public ProductDosesAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> GetAllAsync()
        {
            var productsDoses = await _queryContext.AllProductsDoses.ToListAsync();
            var productsDosesViewModel = productsDoses.Select(r => _mapper.Map<ProductDosesViewModel>(r)).ToList();
            return productsDosesViewModel;
        }

        public async Task<IEnumerable<ProductDosesViewModel>> GetByType(String type)
        {

            var productsDoses = await _queryContext.AllProductsDoses.ToListAsync();
            var productsDosesViewModel = productsDoses
                .Select(r => _mapper.Map<ProductDosesViewModel>(r))
                .Where(r => r.DoseType.Contains(type, StringComparison.InvariantCultureIgnoreCase))
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
