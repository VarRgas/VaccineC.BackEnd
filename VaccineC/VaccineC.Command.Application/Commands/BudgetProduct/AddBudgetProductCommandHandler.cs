using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class AddBudgetProductCommandHandler : IRequestHandler<AddBudgetProductCommand, IEnumerable<BudgetProductViewModel>>
    {
        private readonly IBudgetProductRepository _repository;
        private readonly IBudgetProductAppService _appService;

        public AddBudgetProductCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(AddBudgetProductCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.BudgetProduct newBudgetProduct = new Domain.Entities.BudgetProduct(
                  Guid.NewGuid(),
                  request.BudgetId,
                  request.ProductId,
                  request.BorrowerPersonId,
                  request.ProductDose,
                  request.Details,
                  request.EstimatedSalesValue,
                  request.SituationProduct,
                  DateTime.Now
                  );

            _repository.Add(newBudgetProduct);
            await _repository.SaveChangesAsync();

            return await _appService.GetAllBudgetsProductsByBudgetId(newBudgetProduct.BudgetId);

        }
    }
}
