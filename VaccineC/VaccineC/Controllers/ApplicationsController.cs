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

        //[HttpGet("{name}/GetByName")]
        //public async Task<IActionResult> GetByName(string name)
        //{
        //    var command = new GetApplicationsByPersonNameQuery(name);
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}
    }
}
