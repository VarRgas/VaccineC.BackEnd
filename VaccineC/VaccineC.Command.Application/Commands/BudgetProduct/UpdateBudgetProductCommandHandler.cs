using MediatR;
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

        public UpdateBudgetProductCommandHandler(IBudgetProductRepository repository, IBudgetProductAppService appService)
        {
            _repository = repository;
            _appService = appService;
        }

        public async Task<IEnumerable<BudgetProductViewModel>> Handle(UpdateBudgetProductCommand request, CancellationToken cancellationToken)
        {

            var updatedBudgetProduct = _repository.GetById(request.ID);
            updatedBudgetProduct.SetBorrowerPersonId(request.BorrowerPersonId);
            updatedBudgetProduct.SetProductId(request.ProductId);
            updatedBudgetProduct.SetProductDose(request.ProductDose);
            updatedBudgetProduct.SetDetails(request.Details);
            updatedBudgetProduct.SetEstimatedSalesValue(request.EstimatedSalesValue);
            updatedBudgetProduct.SetSituationProduct(request.SituationProduct);
            updatedBudgetProduct.SetRegister(DateTime.Now);

            await _repository.SaveChangesAsync();

            return await _appService.GetAllBudgetsProductsByBudgetId(updatedBudgetProduct.BudgetId);

        }
    }
}
