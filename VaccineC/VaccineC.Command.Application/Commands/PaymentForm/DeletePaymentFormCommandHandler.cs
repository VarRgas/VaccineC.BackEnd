using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.PaymentForm
{
    internal class DeletePaymentFormCommandHandler : IRequestHandler<DeletePaymentFormCommand, Unit>
    {

        private readonly IPaymentFormRepository _paymentFormRepository;

        public DeletePaymentFormCommandHandler(IPaymentFormRepository paymentFormRepository)
        {
            _paymentFormRepository = paymentFormRepository;
        }

        public async Task<Unit> Handle(DeletePaymentFormCommand request, CancellationToken cancellationToken)
        {
            var paymentForm = _paymentFormRepository.GetById(request.Id);
            _paymentFormRepository.Remove(paymentForm);
            await _paymentFormRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
