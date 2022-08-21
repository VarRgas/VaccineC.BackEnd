using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaccineC.Command.Domain.Abstractions.Repositories;

namespace VaccineC.Command.Application.Commands.PaymentForm
{
    public class AddPaymentFormCommandHandler : IRequestHandler<AddPaymentFormCommand, Unit>
    {

        private readonly IPaymentFormRepository _paymentFormRepository;

        public AddPaymentFormCommandHandler(IPaymentFormRepository paymentFormRepository)
        {
            _paymentFormRepository = paymentFormRepository;
        }

        public async Task<Unit> Handle(AddPaymentFormCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.PaymentForm newPaymentForm = new Domain.Entities.PaymentForm(Guid.NewGuid(), request.Name, request.MaximumInstallments, DateTime.Now);
            _paymentFormRepository.Add(newPaymentForm);
            await _paymentFormRepository.SaveChangesAsync();
            return Unit.Value;
        }
    }
}
