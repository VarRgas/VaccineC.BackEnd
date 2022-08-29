using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.PaymentForm
{
    public class UpdatePaymentFormCommandHandler : IRequestHandler<UpdatePaymentFormCommand, Guid>
    {

        private readonly IPaymentFormRepository _paymentFormRepository;

        public UpdatePaymentFormCommandHandler(IPaymentFormRepository paymentFormRepository)
        {
            _paymentFormRepository = paymentFormRepository;
        }

        public async Task<Guid> Handle(UpdatePaymentFormCommand request, CancellationToken cancellationToken)
        {
            var paymentForm = _paymentFormRepository.GetById(request.ID);
            paymentForm.SetName(request.Name);
            paymentForm.SetMaximumInstallments(request.MaximumInstallments);
            paymentForm.SetRegister(DateTime.Now);

            await _paymentFormRepository.SaveChangesAsync();

            return paymentForm.ID;
        }
    }
}
