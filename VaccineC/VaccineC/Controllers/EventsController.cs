using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Event;
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
            try
            {
                var command = new GetEventListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetEventByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var command = new GetEventListActiveQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<EventsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] EventViewModel eventClass)
        {
            try
            {
                var command = new AddEventCommand(
                    eventClass.ID,
                    eventClass.UserId,
                    eventClass.Situation,
                    eventClass.Concluded,
                    eventClass.StartDate,
                    eventClass.EndDate,
                    eventClass.StartTime,
                    eventClass.EndTime,
                    eventClass.Details,
                    eventClass.Register);

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<EventsController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] EventViewModel eventClass)
        {
            try
            {
                var command = new UpdateEventCommand(
                    id,
                    eventClass.UserId,
                    eventClass.Situation,
                    eventClass.Concluded,
                    eventClass.StartDate,
                    eventClass.EndDate,
                    eventClass.StartTime,
                    eventClass.EndTime,
                    eventClass.Details,
                    eventClass.Register);

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


        // DELETE api/<EventsController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteEventCommand(id);
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
    }
}
