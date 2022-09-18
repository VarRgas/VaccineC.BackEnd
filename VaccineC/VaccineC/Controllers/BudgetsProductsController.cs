using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Queries.BudgetProduct;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BudgetsProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetsProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BudgetsProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetBudgetProductListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<BudgetsProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetBudgetProductByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
