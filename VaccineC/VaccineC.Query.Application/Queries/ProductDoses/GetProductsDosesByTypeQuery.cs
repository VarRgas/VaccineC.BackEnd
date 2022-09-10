using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.ProductDoses
{
    public class GetProductsDosesByTypeQuery : IRequest<IEnumerable<ProductDosesViewModel>>
    {
        public string DoseType { get; set; }

        public GetProductsDosesByTypeQuery(string doseType)
        {
            DoseType = doseType;
        }
    }
}
