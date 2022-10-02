using MediatR;
using VaccineC.Command.Application.Commands.BudgetHistoric;
using VaccineC.Command.Data.Context;
using VaccineC.Command.Domain.Abstractions.Repositories;
using VaccineC.Query.Application.Abstractions;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Command.Application.Commands.BudgetProduct
{
    public class DeleteBudgetProductCommandHandler : IRequestHandler<DeleteBudgetProductCommand, IEnumerable<BudgetProductViewModel>>
    {
        private readonly IBudgetProductRepository _repository;
        private readonly IBudgetProductAppService _appService;
        private readonly IMediator _mediator;

        public DeleteBudgetProductCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService, IMediator mediator)
        {
            _repository = repository;
            _appService = appService;
            _mediator = mediator;   
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(DeleteBudgetProductCommand request, CancellationToken cancellationToken)
        {

            var budgetProduct = _repository.GetById(request.Id);
            var budgetProductViewModel = _appService.GetById(request.Id);
            _repository.Remove(budgetProduct);

            await _repository.SaveChangesAsync();

            string historic = "";

            if (budgetProductViewModel.Person == null) 
            {
                historic = "O Produto " + budgetProductViewModel.Product.Name + " (Sem Tomador) foi removido do orçamento.";
            }
            else
            {
                historic = "O Produto " + budgetProductViewModel.Product.Name + " (Tomador " + budgetProductViewModel.Person.Name + ") foi removido do orçamento.";
            }

            await _mediator.Send(new AddBudgetHistoricCommand(
              Guid.NewGuid(),
              budgetProduct.BudgetId,
              request.UserId,
              historic,
              DateTime.Now
              ));

            return await _appService.GetAllBudgetsProductsByBudgetId(budgetProduct.BudgetId);

        }
    }
}
