using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class MovementProductRepository : RepositoryBase<MovementProduct>, IMovementProductRepository
    {
        public MovementProductRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
