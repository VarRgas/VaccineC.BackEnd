using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Discard;
using VaccineC.Query.Application.Queries.Discard;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscardsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiscardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<DiscardsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetDiscardListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<DiscardsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetDiscardByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<DiscardsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] DiscardViewModel discard)
        {
            try
            {
                var command = new AddDiscardCommand(
                    discard.ID,
                    discard.ProductSummaryBatchId,
                    discard.UserId,
                    discard.Batch, 
                    discard.DiscardedUnits,
                    discard.Reason,
                    discard.Register);

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
