using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Command.Domain.Entities;

namespace VaccineC.Command.Data.Repositories
{
    public class PaymentFormRepository : RepositoryBase<PaymentForm>, IPaymentFormRepository
    {
        public PaymentFormRepository(VaccineCCommandContext db) : base(db)
        {
        }
    }
}
