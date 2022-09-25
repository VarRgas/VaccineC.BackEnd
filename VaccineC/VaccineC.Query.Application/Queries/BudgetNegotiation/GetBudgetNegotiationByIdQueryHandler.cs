using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetNegotiation
{
    public class GetBudgetNegotiationByIdQueryHandler : IRequestHandler<GetBudgetNegotiationByIdQuery, BudgetNegotiationViewModel>
    {

        private readonly IMediator _mediator;

        public GetBudgetNegotiationByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BudgetNegotiationViewModel> Handle(GetBudgetNegotiationByIdQuery request, CancellationToken cancellationToken)
        {
            var budgetsProducts = await _mediator.Send(new GetBudgetNegotiationListQuery());
            var budgetProduct = budgetsProducts.FirstOrDefault(bp => bp.ID == request.Id);
            return budgetProduct;
        }
    }
}
