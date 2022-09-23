using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.Budget;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetBudgetByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
