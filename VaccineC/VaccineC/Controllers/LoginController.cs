using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.Example;

namespace VaccineC.Controllers
{
    [Route("api/LoginController")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ExamplesController>
        [HttpGet]
        public async Task<IActionResult> GetAuthenticatedUsers()
        {
            var command = new GetExampleListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
