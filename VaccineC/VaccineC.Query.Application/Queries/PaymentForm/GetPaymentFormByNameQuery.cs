using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.PaymentForm
{
    public class GetPaymentFormByNameQuery : IRequest<IEnumerable<PaymentFormViewModel>>
    {
        public string Name { get; set; }

        public GetPaymentFormByNameQuery(string name)
        {
            Name = name;
        }
    }
}
