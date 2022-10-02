using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class BudgetHistoricRepository : RepositoryBase<BudgetHistoric>, IBudgetHistoricRepository
    {
        public BudgetHistoricRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
