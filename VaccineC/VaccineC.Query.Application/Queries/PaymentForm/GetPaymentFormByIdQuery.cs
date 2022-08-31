using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PaymentForm
{
    public class GetPaymentFormByIdQuery : IRequest<PaymentFormViewModel>
    {
        public Guid Id;

        public GetPaymentFormByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
