using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class UpdateBudgetProductCommandHandler : IRequestHandler<UpdateBudgetProductCommand, IEnumerable<BudgetProductViewModel>>
    {
        private readonly IBudgetProductRepository _repository;
        private readonly IBudgetProductAppService _appService;
        private readonly IMediator _mediator;

        public UpdateBudgetProductCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _mediator = mediator;   
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(UpdateBudgetProductCommand request, CancellationToken cancellationToken)
        {

            var updatedBudgetProduct = _repository.GetById(request.ID);

            if (updatedBudgetProduct == null)
            {
                throw new ArgumentException("Orçamento Produto não encontrado!");
            }

            updatedBudgetProduct.SetBorrowerPersonId(request.BorrowerPersonId);
            updatedBudgetProduct.SetProductId(request.ProductId);
            updatedBudgetProduct.SetProductDose(request.ProductDose);
            updatedBudgetProduct.SetDetails(request.Details);
            updatedBudgetProduct.SetEstimatedSalesValue(request.EstimatedSalesValue);
            updatedBudgetProduct.SetSituationProduct(request.SituationProduct);
            updatedBudgetProduct.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            var budgetProductViewModel = _appService.GetById(updatedBudgetProduct.ID);

            string historic = "";

            if (budgetProductViewModel.Person == null) {
                historic = "O Produto " + budgetProductViewModel.Product.Name + " (Sem Tomador) teve informações alteradas.";
            }
            else
            {
                historic = "O Produto " + budgetProductViewModel.Product.Name + " (Tomador " + budgetProductViewModel.Person.Name + ") teve informações alteradas.";
            }

            await _mediator.Send(new AddBudgetHistoricCommand(
                Guid.NewGuid(),
                updatedBudgetProduct.BudgetId,
                request.UserId,
                historic,
                DateTime.Now
                ));

            return await _appService.GetAllBudgetsProductsByBudgetId(updatedBudgetProduct.BudgetId);

        }
    }
}
