using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.ProductSummaryBatch
{
    public class AddProductSummaryBatchCommandHandler : IRequestHandler<AddProductSummaryBatchCommand, ProductSummaryBatchViewModel>
    {
        private readonly IProductSummaryBatchRepository _repository;
        private readonly VaccineCCommandContext _ctx;

        public AddProductSummaryBatchCommandHandler(IProductSummaryBatchRepository repository, VaccineCCommandContext ctx)
        {
            _repository = repository;
            _ctx = ctx;
        }

        public async Task<ProductSummaryBatchViewModel> Handle(AddProductSummaryBatchCommand request, CancellationToken cancellationToken)
        {
 
            Domain.Entities.ProductSummaryBatch newProductSummaryBatch = new Domain.Entities.ProductSummaryBatch(
                Guid.NewGuid(),
                request.Batch,
                request.NumberOfUnitsBatch,
                request.ManufacturingDate,
                request.ValidityBatchDate,
                DateTime.Now,
                request.Manufacturer,
                request.ProductsId
                );

            _repository.Add(newProductSummaryBatch);
            await _repository.SaveChangesAsync();

            return new ProductSummaryBatchViewModel()
            {
                ID = newProductSummaryBatch.ID,
                Batch = newProductSummaryBatch.Batch,
                NumberOfUnitsBatch = newProductSummaryBatch.NumberOfUnitsBatch,
                ManufacturingDate = newProductSummaryBatch.ManufacturingDate,
                ValidityBatchDate = newProductSummaryBatch.ValidityBatchDate,
                Manufacturer = newProductSummaryBatch.Manufacturer,
                ProductsId = newProductSummaryBatch.ProductsId,
            };
        }
    }
}
