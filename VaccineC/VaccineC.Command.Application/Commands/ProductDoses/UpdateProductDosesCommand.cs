using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductDoses
{
    public class UpdateProductDosesCommand : IRequest<IEnumerable<ProductDosesViewModel>>
    {
        public Guid ID;
        public Guid ProductsId;
        public string DoseType;
        public int? DoseRangeMonth;
        public DateTime Register;

        public UpdateProductDosesCommand(Guid id, Guid productsId, string doseType, int? doseRangeMonth, DateTime register)
        {
            ID = id;
            ProductsId = productsId;
            DoseType = doseType;
            DoseRangeMonth = doseRangeMonth;
            Register = register;
        }
    }
}
