using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductSummaryBatch
{
    public class UpdateProductSummaryBatchCommand : IRequest<ProductSummaryBatchViewModel>
    {
        public Guid ID;
        public string Batch;
        public decimal NumberOfUnitsBatch;
        public DateTime? ManufacturingDate;
        public DateTime ValidityBatchDate;
        public DateTime Register;
        public string Manufacturer;
        public Guid ProductsId;

        public UpdateProductSummaryBatchCommand(Guid id, string batch, decimal numberOfUnitsBatch, DateTime? manufacturingDate, DateTime validityBatchDate, DateTime register, string manufacturer, Guid productsId)
        {
            ID = id;
            Batch = batch;
            NumberOfUnitsBatch = numberOfUnitsBatch;
            ManufacturingDate = manufacturingDate;
            ValidityBatchDate = validityBatchDate;
            Register = register;
            Manufacturer = manufacturer;
            ProductsId = productsId;
        }
    }
}
