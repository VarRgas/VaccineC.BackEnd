using MediatR;
using System.Threading;
using System.Threading.Tasks;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.PaymentForm
{
    internal class UpdatePaymentFormCommandHandler : IRequestHandler<UpdatePaymentFormCommand, Unit>
    {

        private readonly IPaymentFormRepository _paymentFormRepository;

        public UpdatePaymentFormCommandHandler(IPaymentFormRepository paymentFormRepository)
        {
            _paymentFormRepository = paymentFormRepository;
        }

        public async Task<Unit> Handle(UpdatePaymentFormCommand request, CancellationToken cancellationToken)
        {
            var paymentForm = _paymentFormRepository.GetById(request.ID);
            paymentForm.SetName(request.Name);
            paymentForm.SetMaximumInstallments(request.MaximumInstallments);
            paymentForm.SetRegister(DateTime.Now);

            await _paymentFormRepository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
