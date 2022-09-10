using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class ProductDosesRepository : RepositoryBase<ProductDoses>, IProductDosesRepository
    {
        public ProductDosesRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
