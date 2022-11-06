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
            try
            {
                var command = new GetUserListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<UsersController>/Info/GetByEmail
        [HttpGet("{name}/GetByEmail")]
        public async Task<IActionResult> GetByEmail(string name)
        {
            try
            {
                var command = new GetUserByEmailQuery(name);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<UsersController>/GetAllActive
        [HttpGet, Route("GetAllActive")]
        public async Task<IActionResult> GetAllActive()
        {
            try
            {
                var command = new GetUserListActiveQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetUserByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<UsersController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteUserCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Existem informações vinculadas a este usuário que impedem sua exclusão.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UsersController>/3/ActivateSituation
        [HttpPost("{id}/ActivateSituation")]
        public async Task<IActionResult> ActivateSituation(Guid id)
        {
            try
            {
                var command = new UpdateActivateUserSituationCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UsersController>/3/DeactivateSituation
        [HttpPost("{id}/DeactivateSituation")]
        public async Task<IActionResult> DeactivateSituation(Guid id)
        {
            try
            {
                var command = new UpdateDeactivateUserSituationCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<UsersController>/3/ResetPassword
        [HttpPost("{id}/ResetPassword")]
        public async Task<IActionResult> ResetPassword(Guid id, [FromBody] ResetPasswordUserCommand query)
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
