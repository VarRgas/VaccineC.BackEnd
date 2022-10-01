using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class DiscardRepository : RepositoryBase<Discard>, IDiscardRepository
    {
        public DiscardRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
