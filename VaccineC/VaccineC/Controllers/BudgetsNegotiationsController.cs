using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.BudgetNegotiation;
using VaccineC.Query.Application.Queries.BudgetNegotiation;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetsNegotiationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetsNegotiationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BudgetsNegotiationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetBudgetNegotiationListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<BudgetsNegotiationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetBudgetNegotiationByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<BudgetsNegotiationsController>/5/GetBudgetsNegotiationsByBudget
        [HttpGet("{budgetId}/GetBudgetsNegotiationsByBudget")]
        public async Task<IActionResult> GetAllBudgetsNegotiationsByBudgetID(Guid budgetId)
        {
            var command = new GetBudgetNegotiationListByBudgetQuery(budgetId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<BudgetsNegotiationsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BudgetNegotiationViewModel budgetNegotiationViewModel)
        {
            try
            {
                var command = new AddBudgetNegotiationCommand(
                    budgetNegotiationViewModel.ID,
                    budgetNegotiationViewModel.BudgetId,
                    budgetNegotiationViewModel.PaymentFormId,
                    budgetNegotiationViewModel.TotalAmountBalance,
                    budgetNegotiationViewModel.TotalAmountTraded,
                    budgetNegotiationViewModel.Installments,
                    budgetNegotiationViewModel.Register

                    );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BudgetsNegotiationsController>/5/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteBudgetNegotiationCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BudgetsNegotiationsController>/DeleteOnDemand
        [HttpPost("DeleteOnDemand")]
        public async Task<IActionResult> DeleteOnDemand([FromBody] List<BudgetNegotiationViewModel> listBudgetNegotiationViewModel)
        {
            try
            {
                var command = new DeleteBudgetNegotiationOnDemandCommand(listBudgetNegotiationViewModel);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
