using MediatR;
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

        public DeleteBudgetProductCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(DeleteBudgetProductCommand request, CancellationToken cancellationToken)
        {

            var budgetProduct = _repository.GetById(request.Id);
            _repository.Remove(budgetProduct);

            await _repository.SaveChangesAsync();

            return await _appService.GetAllBudgetsProductsByBudgetId(budgetProduct.BudgetId);

        }
    }
}
