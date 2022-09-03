using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class PersonAddressRepository : RepositoryBase<PersonAddress>, IPersonAddressRepository
    {
        public PersonAddressRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
