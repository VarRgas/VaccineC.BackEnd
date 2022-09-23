using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.Budget
{
    public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetViewModel>
    {

        private readonly IMediator _mediator;

        public GetBudgetByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BudgetViewModel> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
        {
            var budgets = await _mediator.Send(new GetBudgetListQuery());
            var budget = budgets.FirstOrDefault(b => b.ID == request.Id);
            return budget;
        }
    }
}
