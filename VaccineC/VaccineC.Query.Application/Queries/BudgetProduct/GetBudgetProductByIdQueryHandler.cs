using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetProduct
{
    public class GetBudgetProductByIdQueryHandler : IRequestHandler<GetBudgetProductByIdQuery, BudgetProductViewModel>
    {

        private readonly IMediator _mediator;

        public GetBudgetProductByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BudgetProductViewModel> Handle(GetBudgetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var budgetsProducts = await _mediator.Send(new GetBudgetProductListQuery());
            var budgetProduct = budgetsProducts.FirstOrDefault(bp => bp.ID == request.Id);
            return budgetProduct;
        }
    }
}
