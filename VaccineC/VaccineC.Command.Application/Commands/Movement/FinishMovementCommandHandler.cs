using AutoMapper;
using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.ViewModels;
using VaccineC.Query.Model.Abstractions;

namespace VaccineC.Command.Application.Commands.Movement
{
    public class FinishMovementCommandHandler : IRequestHandler<FinishMovementCommand, MovementViewModel>
    {
        private readonly IMovementRepository _movementRepository;
        private readonly VaccineCCommandContext _ctx;
        private readonly IQueryContext _queryContext;
        private readonly IMapper _mapper;
        private readonly IProductSummaryBatchRepository _productSummaryBatchRepository;

        public FinishMovementCommandHandler(IMovementRepository movementRepository, VaccineCCommandContext ctx, IQueryContext queryContext, IMapper mapper, IProductSummaryBatchRepository productSummaryBatchRepository)
        {
            _movementRepository = movementRepository;
            _ctx = ctx;
            _mapper = mapper;
            _queryContext = queryContext;
            _productSummaryBatchRepository = productSummaryBatchRepository;
        }

        public async Task<MovementViewModel> Handle(FinishMovementCommand request, CancellationToken cancellationToken)
        {

            var movement = _movementRepository.GetById(request.ID);

            List<Domain.Entities.MovementProduct> listMovementProductViewModel = await this.getMovementsProductsByMovement(movement);

            await this.validateMovementProduct(listMovementProductViewModel);

            foreach (var movementProduct in listMovementProductViewModel)
            {
                var productSummaryBatch = _ctx.ProductsSummariesBatches
                    .Where(pmb => pmb.Batch.Equals(movementProduct.Batch) && pmb.ManufacturingDate.Equals(movementProduct.BatchManufacturingDate))
                    .FirstOrDefault();

                if (productSummaryBatch == null && movement.MovementType.Equals("E"))
                {

                    Domain.Entities.ProductSummaryBatch newProductSummaryBatch = new Domain.Entities.ProductSummaryBatch(

                        Guid.NewGuid(),
                        movementProduct.Batch,
                        movementProduct.UnitsNumber,
                        movementProduct.BatchManufacturingDate,
                        movementProduct.BatchExpirationDate,
                        DateTime.Now,
                        movementProduct.Manufacturer,
                        movementProduct.ProductId
                        );

                    _productSummaryBatchRepository.Add(newProductSummaryBatch);
                    await _productSummaryBatchRepository.SaveChangesAsync();
                }
                else
                {

                    if (movement.MovementType.Equals("E"))
                    {
                        productSummaryBatch.SetNumberOfUnitsBatch(productSummaryBatch.NumberOfUnitsBatch + movementProduct.UnitsNumber);
                    }
                    else
                    {
                        if (productSummaryBatch.NumberOfUnitsBatch < movementProduct.UnitsNumber)
                        {
                            throw new ArgumentException("Não é possível retirar " + movementProduct.UnitsNumber + " unidades do lote " + productSummaryBatch.Batch + ", pois o total de unidades presentes é " + productSummaryBatch.NumberOfUnitsBatch);
                        }

                        productSummaryBatch.SetNumberOfUnitsBatch(productSummaryBatch.NumberOfUnitsBatch - movementProduct.UnitsNumber);

                    }
                    productSummaryBatch.SetRegister(DateTime.Now);
                    await _productSummaryBatchRepository.SaveChangesAsync();
                }
            }

            movement.SetSituation("F");
            movement.SetUsersId(request.UsersId);
            movement.SetProductsAmount(request.ProductsAmount);
            movement.SetRegister(DateTime.Now);

            await _movementRepository.SaveChangesAsync();

            return new MovementViewModel()
            {
                ID = movement.ID,
                MovementNumber = movement.MovementNumber,
                UsersId = movement.UsersId,
                MovementType = movement.MovementType,
                ProductsAmount = movement.ProductsAmount,
                Situation = movement.Situation
            };
        }

        private async Task<Boolean> validateMovementProduct(List<Domain.Entities.MovementProduct> listMovementProductViewModel)
        {
            if (listMovementProductViewModel.Count == 0)
            {
                throw new ArgumentException("Não é possível finalizar um movimento sem Produtos!");
            }
            else
            {
                return true;
            }
        }

        private async Task<List<Domain.Entities.MovementProduct>> getMovementsProductsByMovement(Domain.Entities.Movement movement)
        {

            List<Domain.Entities.MovementProduct> movementsProducts = (from mp in _ctx.MovementsProducts
                                                                       where mp.MovementId == movement.ID
                                                                       select mp).ToList();

            return movementsProducts;
        }

    }
}
