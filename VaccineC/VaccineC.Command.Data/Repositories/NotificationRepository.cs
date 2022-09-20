using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
