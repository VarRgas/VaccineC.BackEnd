using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class DiscardAppService : IDiscardAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public DiscardAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DiscardViewModel>> GetAllAsync()
        {
            var discards = await _queryContext.AllDiscards.ToListAsync();
            var discardsViewModel = discards.Select(r => _mapper.Map<DiscardViewModel>(r)).ToList();
            return discardsViewModel;
        }

        public DiscardViewModel GetById(Guid id)
        {
            var discard = _mapper.Map<DiscardViewModel>(_queryContext.AllDiscards.Where(r => r.ID == id).First());
            return discard;
        }
    }
}
