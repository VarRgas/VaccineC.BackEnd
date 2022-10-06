using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.Discard
{
    public class AddDiscardCommand : IRequest
    {
        public Guid ID;
        public Guid ProductSummaryBatchId;
        public Guid UserId;
        public string Batch;
        public int DiscardedUnits;
        public string Reason;
        public DateTime Register;

        public AddDiscardCommand(Guid id, Guid productSummaryBatchId, Guid userId, string batch, int discardedUnits, string reason, DateTime register)
        {
            ID = id;
            ProductSummaryBatchId = productSummaryBatchId;
            UserId = userId;
            Batch = batch;
            DiscardedUnits = discardedUnits;
            Reason = reason;
            Register = register;
        }
    }
}
