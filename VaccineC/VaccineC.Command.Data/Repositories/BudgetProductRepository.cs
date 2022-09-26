using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class BudgetProductRepository : RepositoryBase<BudgetProduct>, IBudgetProductRepository
    {
        public BudgetProductRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
