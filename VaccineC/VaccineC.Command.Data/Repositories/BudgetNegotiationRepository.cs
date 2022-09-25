using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;


namespace VaccineC.Command.Data.Repositories
{
    public class BudgetNegotiationRepository : RepositoryBase<BudgetNegotiation>, IBudgetNegotiationRepository
    {
        public BudgetNegotiationRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
