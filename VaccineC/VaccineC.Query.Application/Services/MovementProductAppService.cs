using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class MovementProductAppService : IMovementProductAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;

        public MovementProductAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<MovementProductViewModel>> GetAllAsync()
        {

            var movementsProducts = await _queryContext.AllMovementsProducts.ToListAsync();
            var movementsProductsViewModel = movementsProducts.Select(r => _mapper.Map<MovementProductViewModel>(r)).ToList();
            return movementsProductsViewModel;
        }

        public async Task<IEnumerable<MovementProductViewModel>> GetAllByMovementId(Guid movementId)
        {
            var movementsProducts = await _queryContext.AllMovementsProducts.ToListAsync();
            var movementsProductsViewModel = movementsProducts
                .Select(r => _mapper.Map<MovementProductViewModel>(r))
                .Where(r => r.MovementId == movementId)
                .ToList();
            return movementsProductsViewModel;
        }
    }
}
