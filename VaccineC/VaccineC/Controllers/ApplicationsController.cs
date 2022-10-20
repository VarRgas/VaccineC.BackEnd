using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.Application;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetApplicationListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetApplicationByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personId}/GetPersonApplicationNumber")]
        public async Task<IActionResult> GetPersonApplicationNumber(Guid personId)
        {
            var command = new GetApplicationNumberByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personId}/GetAvailableApplicationsByPersonId")]
        public async Task<IActionResult> GetAvailableApplicationsByPersonId(Guid personId)
        {
            var command = new GetHistoryApplicationsByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personId}/GetHistoryApplicationsByPersonId")]
        public async Task<IActionResult> GetHistoryApplicationsByPersonId(Guid personId)
        {
            var command = new GetHistoryApplicationsByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
