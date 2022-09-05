using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class PersonJuridicalRepository : RepositoryBase<PersonsJuridical>, IPersonJuridicalRepository
    {
        public PersonJuridicalRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
