using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Budget;
using VaccineC.Query.Application.Queries.Budget;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BudgetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetBudgetListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personName}/GetByPersonName")]
        public async Task<IActionResult> GetBudgetsByPersonName(string personName)
        {
            var command = new GetBudgetByPersonNameQuery(personName);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{borrowerId}/GetAllByBorrower")]
        public async Task<IActionResult> GetAllByBorrower(Guid borrowerId)
        {
            var command = new GetBudgetListByBorrowerQuery(borrowerId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{responsibleId}/GetAllByResponsible")]
        public async Task<IActionResult> GetAllByResponsible(Guid responsibleId)
        {
            var command = new GetBudgetListByResponsibleQuery(responsibleId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetBudgetByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BudgetViewModel budget)
        {
            try
            {
                var command = new AddBudgetCommand(budget.ID, budget.UserId, budget.PersonId, budget.Situation, budget.DiscountPercentage, budget.DiscountValue, budget.TotalBudgetAmount,
                    budget.TotalBudgetedAmount, budget.ExpirationDate, budget.ApprovalDate, budget.Details, budget.BudgetNumber, budget.Register);

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BudgetViewModel budget)
        {
            try
            {
                var command = new UpdateBudgetCommand(id, 
                                                      budget.UserId, 
                                                      budget.PersonId, 
                                                      budget.Situation, 
                                                      budget.DiscountPercentage, 
                                                      budget.DiscountValue, 
                                                      budget.TotalBudgetAmount,
                                                      budget.TotalBudgetedAmount, 
                                                      budget.ExpirationDate, 
                                                      budget.ApprovalDate, 
                                                      budget.Details, 
                                                      budget.BudgetNumber, 
                                                      budget.Register);

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
