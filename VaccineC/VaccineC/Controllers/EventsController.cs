using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Queries.Event;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<EventsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetEventListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetEventByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
