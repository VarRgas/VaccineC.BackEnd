using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class AddBudgetProductOnDemandCommandHandler : IRequestHandler<AddBudgetProductOnDemandCommand, IEnumerable<BudgetProductViewModel>>
    {
        private readonly IBudgetProductRepository _repository;
        private readonly IBudgetProductAppService _appService;

        public AddBudgetProductOnDemandCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(AddBudgetProductOnDemandCommand request, CancellationToken cancellationToken)
        {
            Guid budgetId = Guid.NewGuid();

            List<BudgetProductViewModel> listBudgetProductViewModel = request.ListBudgetProdcutViewModel;

            foreach(var budgetProduct in listBudgetProductViewModel)
            {
                Domain.Entities.BudgetProduct newBudgetProduct = new Domain.Entities.BudgetProduct(
                      Guid.NewGuid(),
                      budgetProduct.BudgetId,
                      budgetProduct.ProductId,
                      budgetProduct.BorrowerPersonId,
                      budgetProduct.ProductDose,
                      budgetProduct.Details,
                      budgetProduct.EstimatedSalesValue,
                      budgetProduct.SituationProduct,
                      DateTime.Now
                      );

                _repository.Add(newBudgetProduct);
                budgetId = newBudgetProduct.BudgetId;
                await _repository.SaveChangesAsync();
            }

            return await _appService.GetAllBudgetsProductsByBudgetId(budgetId);
        }
    }
}
