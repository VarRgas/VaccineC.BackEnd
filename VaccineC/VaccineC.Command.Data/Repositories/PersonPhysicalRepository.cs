using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class PersonPhysicalRepository : RepositoryBase<PersonsPhysical>, IPersonPhysicalRepository
    {
        public PersonPhysicalRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
