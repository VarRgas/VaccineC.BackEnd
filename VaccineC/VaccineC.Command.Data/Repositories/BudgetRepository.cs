using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class BudgetRepository : RepositoryBase<Budget>, IBudgetRepository
    {
        public BudgetRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
