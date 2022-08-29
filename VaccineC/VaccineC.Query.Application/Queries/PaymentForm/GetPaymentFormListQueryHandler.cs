using MediatR;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PaymentForm
{
    public class GetPaymentFormListQueryHandler : IRequestHandler<GetPaymentFormListQuery, IEnumerable<PaymentFormViewModel>>
    {

        private readonly IPaymentFormAppService _paymentFormAppService;

        public GetPaymentFormListQueryHandler(IPaymentFormAppService paymentFormAppService)
        {
            _paymentFormAppService = paymentFormAppService;
        }

        public async Task<IEnumerable<PaymentFormViewModel>> Handle(GetPaymentFormListQuery request, CancellationToken cancellationToken)
        {
            return await _paymentFormAppService.GetAllAsync();
        }
    }
}
