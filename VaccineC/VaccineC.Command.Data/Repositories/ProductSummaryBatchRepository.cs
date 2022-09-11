using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class ProductSummaryBatchRepository : RepositoryBase<ProductSummaryBatch>, IProductSummaryBatchRepository
    {
        public ProductSummaryBatchRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
