using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class PersonPhoneRepository : RepositoryBase<PersonPhone>, IPersonPhoneRepository
    {
        public PersonPhoneRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
