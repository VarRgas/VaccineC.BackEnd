using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Command.Application.Commands.UserCommands;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] AddUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
