using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;


namespace VaccineC.Command.Data.Repositories
{
    public class CompanyParameterRepository : RepositoryBase<CompanyParameter>, ICompanyParameterRepository
    {
        public CompanyParameterRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
