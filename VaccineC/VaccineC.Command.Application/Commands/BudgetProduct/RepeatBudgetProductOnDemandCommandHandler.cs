using MediatR;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class RepeatBudgetProductOnDemandCommandHandler : IRequestHandler<RepeatBudgetProductOnDemandCommand, IEnumerable<BudgetProductViewModel>>
    {
        private readonly IBudgetProductRepository _repository;
        private readonly IBudgetProductAppService _appService;

        public RepeatBudgetProductOnDemandCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(RepeatBudgetProductOnDemandCommand request, CancellationToken cancellationToken)
        {
            if (request.NumberOfTimes <= 0) {
                throw new ArgumentException("O Nº de Vezes deve ser maior que 0!");
            }

            var budgetProduct = _repository.GetById(request.BudgetProductId);

            if (budgetProduct == null)
            {
                throw new ArgumentException("Produto Orçamento não encontrado!");

            }

            Guid? borrowerId = Guid.Empty;

            if (request.RepeatBorrower) {
                borrowerId = budgetProduct.BorrowerPersonId;
            }
            else{
                borrowerId = null;
            }

            for (int i = 0; i < request.NumberOfTimes; i++)
            {
                
                Domain.Entities.BudgetProduct repeatedBudgetProduct = new Domain.Entities.BudgetProduct(
                 Guid.NewGuid(),
                 budgetProduct.BudgetId,
                 budgetProduct.ProductId,
                 borrowerId,
                 budgetProduct.ProductDose,
                 budgetProduct.Details,
                 budgetProduct.EstimatedSalesValue,
                 budgetProduct.SituationProduct,
                 DateTime.Now
                 );

                _repository.Add(repeatedBudgetProduct);
                await _repository.SaveChangesAsync();
            }

            return await _appService.GetAllBudgetsProductsByBudgetId(budgetProduct.BudgetId);

        }
    }
}
