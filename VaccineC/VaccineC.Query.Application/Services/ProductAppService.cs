using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public ProductAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
        {
            var products = await _queryContext.AllProducts.ToListAsync();
            var productsViewModel = products.Select(r => _mapper.Map<ProductViewModel>(r)).ToList();
            return productsViewModel;
        }

        public async Task<IEnumerable<ProductViewModel>> GetByName(String name)
        {

            var products = await _queryContext.AllProducts.ToListAsync();
            var productsViewModel = products
                .Select(r => _mapper.Map<ProductViewModel>(r))
                .Where(r => r.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
            return productsViewModel;

        }

        public ProductViewModel GetById(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(_queryContext.AllProducts
                                 .Where(r => r.ID == id)
                                 .First());
            return product;
        }

        public Task<IEnumerable<SbimVaccinesViewModel>> GetAllVaccinesAutocomplete()
        {

            List<SbimVaccines> vaccines = (from p in _context.SbimVaccines
                                           select p).ToList();

            var response = _mapper.Map<IEnumerable<SbimVaccinesViewModel>>(vaccines);

            return Task.FromResult(response);
        }
    }
}
