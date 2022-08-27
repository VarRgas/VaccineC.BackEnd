using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Query.Application.Services
{
    public class PersonAppService : IPersonAppService
    {
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;

        public PersonAppService(IQueryContext queryContext, IMapper mapper)
        {
            _queryContext = queryContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllAsync()
        {
            var persons = await _queryContext.AllPersons.ToListAsync();
            var personsViewModel = persons.Select(p => _mapper.Map<PersonViewModel>(p)).ToList();
            return personsViewModel;
        }

        public async Task<IEnumerable<PersonViewModel>> GetAllByType(string personType)
        {
            var persons = await _queryContext.AllPersons.ToListAsync();
            var personsViewModel = persons
                .Select(p => _mapper.Map<PersonViewModel>(p))
                .Where(p => p.PersonType.Equals(personType))
                .ToList();
            return personsViewModel;
        }
    }
}
