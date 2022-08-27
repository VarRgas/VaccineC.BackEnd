using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.UserCommands;
using VaccineC.Query.Application.Queries.User;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetUserListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<UsersController>/Info/GetByEmail
        [HttpGet("{name}/GetByEmail")]
        public async Task<IActionResult> GetByEmail(string name)
        {
            var command = new GetUserByEmailQuery(name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<UsersController>/GetAllActive
        [HttpGet, Route("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            var command = new GetUserListActiveQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetUserByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        // POST api/<UsersController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserViewModel user)
        {
            try
            {
                var command = new AddUserCommand(
                    user.ID,
                    user.PersonId,
                    user.Email,
                    user.Password,
                    user.Situation,
                    user.FunctionUser,
                    user.Register
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<UsersController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UserViewModel user)
        {
            try
            {
                var command = new UpdateUserCommand(
                    id,
                    user.PersonId,
                    user.Email,
                    user.Password,
                    user.Situation,
                    user.FunctionUser,
                    user.Register
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        // DELETE api/<UsersController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
