using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PaymentForm
{
    public class GetPaymentFormByNameQueryHandler : IRequestHandler<GetPaymentFormByNameQuery, IEnumerable<PaymentFormViewModel>>
    {
        private readonly IPaymentFormAppService _paymentFormAppService;

        public GetPaymentFormByNameQueryHandler(IPaymentFormAppService paymentFormAppService)
        {
            _paymentFormAppService = paymentFormAppService;
        }

        public async Task<IEnumerable<PaymentFormViewModel>> Handle(GetPaymentFormByNameQuery request, CancellationToken cancellationToken)
        {
            return await _paymentFormAppService.GetByName(request.Name);
        }

    }
}
