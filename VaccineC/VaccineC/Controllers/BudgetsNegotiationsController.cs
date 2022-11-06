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
            try
            {
                var command = new GetBudgetNegotiationListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<BudgetsNegotiationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetBudgetNegotiationByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<BudgetsNegotiationsController>/5/GetBudgetsNegotiationsByBudget
        [HttpGet("{budgetId}/GetBudgetsNegotiationsByBudget")]
        public async Task<IActionResult> GetAllBudgetsNegotiationsByBudgetID(Guid budgetId)
        {
            try
            {
                var command = new GetBudgetNegotiationListByBudgetQuery(budgetId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
                    budgetNegotiationViewModel.Register,
                    budgetNegotiationViewModel.UserId
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
        [HttpDelete("{id}/{userId}/Delete")]
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            try
            {
                var command = new DeleteBudgetNegotiationCommand(id, userId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BudgetsNegotiationsController>/DeleteOnDemand
        [HttpPost("{userId}/DeleteOnDemand")]
        public async Task<IActionResult> DeleteOnDemand([FromBody] List<BudgetNegotiationViewModel> listBudgetNegotiationViewModel, Guid? userId)
        {
            try
            {
                var command = new DeleteBudgetNegotiationOnDemandCommand(listBudgetNegotiationViewModel, userId);
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
