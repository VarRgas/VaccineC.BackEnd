using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
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
        private readonly IMediator _mediator;

        public AddBudgetProductOnDemandCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _mediator = mediator;   
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(AddBudgetProductOnDemandCommand request, CancellationToken cancellationToken)
        {
            Guid budgetId = Guid.NewGuid();
            Guid? userId = Guid.NewGuid();
            string products = "";

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
                userId = budgetProduct.UserId;

                await _repository.SaveChangesAsync();
            }

            string historic = "";

            if (listBudgetProductViewModel.Count > 1) {
                historic = "Foram adicionados " + listBudgetProductViewModel.Count + " novos produtos ao orçamento.";
            }
            else
            {
                historic = "Novo produto adicionado ao orçamento.";
            }

            await _mediator.Send(new AddBudgetHistoricCommand(
                Guid.NewGuid(),
                budgetId,
                userId,
                historic,
                DateTime.Now
                ));

            return await _appService.GetAllBudgetsProductsByBudgetId(budgetId);
        }
    }
}
