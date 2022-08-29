using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class UserResourceRepository : RepositoryBase<UserResource>, IUserResourceRepository
    {
        public UserResourceRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
