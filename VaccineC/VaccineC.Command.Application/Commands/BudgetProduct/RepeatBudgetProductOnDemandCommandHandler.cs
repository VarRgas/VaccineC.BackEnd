using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
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
        private readonly IMediator _mediator;

        public RepeatBudgetProductOnDemandCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _mediator = mediator;   
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(RepeatBudgetProductOnDemandCommand request, CancellationToken cancellationToken)
        {
            if (request.NumberOfTimes <= 0) {
                throw new ArgumentException("O Nº de Vezes deve ser maior que 0!");
            }

            var budgetProductViewModel = _appService.GetById(request.BudgetProductId);

            if (budgetProductViewModel == null)
            {
                throw new ArgumentException("Orçamento Produto não encontrado!");

            }

            Guid? borrowerId = Guid.Empty;

            if (request.RepeatBorrower) {
                borrowerId = budgetProductViewModel.BorrowerPersonId;
            }
            else{
                borrowerId = null;
            }

            for (int i = 0; i < request.NumberOfTimes; i++)
            {
                
                Domain.Entities.BudgetProduct repeatedBudgetProduct = new Domain.Entities.BudgetProduct(
                 Guid.NewGuid(),
                 budgetProductViewModel.BudgetId,
                 budgetProductViewModel.ProductId,
                 borrowerId,
                 budgetProductViewModel.ProductDose,
                 budgetProductViewModel.Details,
                 budgetProductViewModel.EstimatedSalesValue,
                 budgetProductViewModel.SituationProduct,
                 DateTime.Now
                 );

                _repository.Add(repeatedBudgetProduct);
                await _repository.SaveChangesAsync();
            }

            await _mediator.Send(new AddBudgetHistoricCommand(
                 Guid.NewGuid(),
                 budgetProductViewModel.BudgetId,
                 request.UserId,
                 "O Produto " + budgetProductViewModel.Product.Name + " foi repetido " + request.NumberOfTimes + " vez(es) no orçamento.",
                 DateTime.Now
                 ));

            return await _appService.GetAllBudgetsProductsByBudgetId(budgetProductViewModel.BudgetId);

        }
    }
}
