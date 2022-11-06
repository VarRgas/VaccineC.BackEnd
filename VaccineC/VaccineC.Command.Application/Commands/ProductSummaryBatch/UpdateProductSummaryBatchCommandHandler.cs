using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductSummaryBatch
{
    public class UpdateProductSummaryBatchCommandHandler : IRequestHandler<UpdateProductSummaryBatchCommand, ProductSummaryBatchViewModel>
    {
        private readonly IProductSummaryBatchRepository _repository;
        private readonly VaccineCCommandContext _ctx;

        public UpdateProductSummaryBatchCommandHandler(IProductSummaryBatchRepository repository, VaccineCCommandContext ctx)
        {
            _repository = repository;
            _ctx = ctx;
        }

        public async Task<ProductSummaryBatchViewModel> Handle(UpdateProductSummaryBatchCommand request, CancellationToken cancellationToken)
        {

            var productSummaryBatch = _repository.GetById(request.ID);

            if (productSummaryBatch == null)
            {
                throw new ArgumentException("Produto Lote não encontrado!");
            }

            productSummaryBatch.SetBatch(request.Batch);
            productSummaryBatch.SetNumberOfUnitsBatch(request.NumberOfUnitsBatch);
            productSummaryBatch.SetManufacturingDate(request.ManufacturingDate);
            productSummaryBatch.SetValidityBatchDate(request.ValidityBatchDate);
            productSummaryBatch.SetRegister(DateTime.Now);
            productSummaryBatch.SetManufacturer(request.Manufacturer);
            productSummaryBatch.SetProductsId(request.ProductsId);

            await _repository.SaveChangesAsync();

            return new ProductSummaryBatchViewModel()
            {
                ID = productSummaryBatch.ID,
                Batch = productSummaryBatch.Batch,
                NumberOfUnitsBatch = productSummaryBatch.NumberOfUnitsBatch,
                ManufacturingDate = productSummaryBatch.ManufacturingDate,
                ValidityBatchDate = productSummaryBatch.ValidityBatchDate,
                Manufacturer = productSummaryBatch.Manufacturer,
                ProductsId = productSummaryBatch.ProductsId,
            };
        }
    }
}
