using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PaymentForm
{
    public class GetPaymentFormByIdQueryHandler : IRequestHandler<GetPaymentFormByIdQuery, PaymentFormViewModel>
    {
        private readonly IMediator _mediator;

        public GetPaymentFormByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<PaymentFormViewModel> Handle(GetPaymentFormByIdQuery request, CancellationToken cancellationToken)
        {
            var paymentForms = await _mediator.Send(new GetPaymentFormListQuery());
            var paymentForm = paymentForms.FirstOrDefault(pf => pf.ID == request.Id);
            return paymentForm;
        }
    }
}
