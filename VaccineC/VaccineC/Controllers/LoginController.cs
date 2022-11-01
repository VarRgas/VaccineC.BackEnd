using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.Login;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class LoginController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LoginController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginQuery query)
        {
            try
            {
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
