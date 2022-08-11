using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class ExampleAppService : ILoginAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        public ExampleAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ExampleViewModel>> GetAllAsync()
        {
            var roomTypes = await _queryContext.AllExamples.ToListAsync();
            var roomTypesViewModel = roomTypes.Select(r => _mapper.Map<ExampleViewModel>(r)).ToList();
            return roomTypesViewModel;
        }

        public ExampleViewModel GetById(int id)
        {
            var roomType = _mapper.Map<ExampleViewModel>(_queryContext.AllExamples.Where(r => r.Id == id).First());
            return roomType;
        }
    }
}
