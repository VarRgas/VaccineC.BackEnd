using MediatR;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.PaymentForm
{
    public class AddPaymentFormCommandHandler : IRequestHandler<AddPaymentFormCommand, Guid>
    {

        private readonly IPaymentFormRepository _paymentFormRepository;

        public AddPaymentFormCommandHandler(IPaymentFormRepository paymentFormRepository)
        {
            _paymentFormRepository = paymentFormRepository;
        }

        public async Task<Guid> Handle(AddPaymentFormCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.PaymentForm newPaymentForm = new Domain.Entities.PaymentForm(Guid.NewGuid(), request.Name, request.MaximumInstallments, DateTime.Now);
            _paymentFormRepository.Add(newPaymentForm);
            await _paymentFormRepository.SaveChangesAsync();
            return newPaymentForm.ID;
        }
    }
}
