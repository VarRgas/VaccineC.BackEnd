using MediatR;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Query.Application.Queries.BudgetHistoric
{
    public class GetBudgetHistoricByIdQueryHandler : IRequestHandler<GetBudgetHistoricByIdQuery, BudgetHistoricViewModel>
    {

        private readonly IMediator _mediator;

        public GetBudgetHistoricByIdQueryHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BudgetHistoricViewModel> Handle(GetBudgetHistoricByIdQuery request, CancellationToken cancellationToken)
        {
            var budgetsHistorics = await _mediator.Send(new GetBudgetHistoricListQuery());
            var budgetHistoric = budgetsHistorics.FirstOrDefault(bh => bh.ID == request.Id);
            return budgetHistoric;
        }
    }
}
