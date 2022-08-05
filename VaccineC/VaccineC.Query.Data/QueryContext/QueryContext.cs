using VaccineC.Query.Data.Context;
using VaccineC.Query.Model.Abstractions;
using VaccineC.Query.Model.Models;

namespace VaccineC.Query.Data.QueryContext
{
    public class QueryContext : IQueryContext
    {
        private readonly VaccineCContext _context;

        public QueryContext(VaccineCContext context)
        {
            _context = context;
        }



        public IQueryable<Example> AllExamples
        {
            get
            {
                return _context
                .Set<Example>();
            }
        }
    }
}
