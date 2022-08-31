using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class CompanyScheduleRepository : RepositoryBase<CompanySchedule>, ICompanyScheduleRepository
    {
        public CompanyScheduleRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
