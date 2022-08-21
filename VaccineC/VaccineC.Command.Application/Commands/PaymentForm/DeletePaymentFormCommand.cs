using MediatR;

namespace VaccineC.Command.Application.Commands.PaymentForm
{
    public class DeletePaymentFormCommand : IRequest
    {
        public Guid Id;

        public DeletePaymentFormCommand(Guid id)
        {
            Id = id;
        }
    }
}
