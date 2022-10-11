using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class AuthorizationNotificationRepository : RepositoryBase<AuthorizationNotification>, IAuthorizationNotificationRepository
    {
        public AuthorizationNotificationRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
