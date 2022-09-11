using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Application.Services
{
    public class MovementAppService : IMovementAppService
    {

        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly VaccineCContext _context;


        public MovementAppService(IQueryContext queryContext, IMapper mapper, VaccineCContext context)
        {
            _queryContext = queryContext;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<MovementViewModel>> GetAllAsync()
        {
            var movements = await _queryContext.AllMovements.ToListAsync();
            var movementsViewModel = movements.Select(r => _mapper.Map<MovementViewModel>(r)).ToList();
            return movementsViewModel;
        }

        public async Task<IEnumerable<MovementViewModel>> GetAllByMovementNumber(int movementNumber)
        {
            var movements = await _queryContext.AllMovements.ToListAsync();
            var movementsViewModel = movements
                .Select(r => _mapper.Map<MovementViewModel>(r))
                .Where(r => r.MovementNumber == movementNumber)
                .ToList();
            return movementsViewModel;
        }

        public Task<IEnumerable<MovementViewModel>> GetAllByProductName(string productName)
        {
            /*
               List<Person> persons = (from p in _context.Persons
                                    join u in _context.Users on p.ID equals u.PersonId into _u
                                    from x in _u.DefaultIfEmpty()
                                    where p.PersonType.Equals("F")
                                    where x.PersonId.Equals(null)
                                    select p).ToList();

            var response = _mapper.Map<IEnumerable<PersonViewModel>>(persons);

            return Task.FromResult(response);
             */


            throw new NotImplementedException();
        }
    }
}
