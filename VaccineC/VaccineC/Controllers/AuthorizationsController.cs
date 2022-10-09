using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Authorization;
using VaccineC.Query.Application.Queries.Authorization;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthorizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthorizationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAuthorizationListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<AuthorizationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetAuthorizationByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<AuthorizationsController>/GetSummarySituationAuthorization
        [HttpGet("GetSummarySituationAuthorization")]
        public async Task<IActionResult> getSummarySituationAuthorization()
        {
            var command = new GetSummarySituationAuthorizationQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<AuthorizationsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AuthorizationViewModel authorization)
        {
            try
            {
                var command = new AddAuthorizationCommand(
                    authorization.ID,
                    authorization.UserId,
                    authorization.EventId,
                    authorization.BudgetProductId,
                    authorization.BorrowerPersonId,
                    authorization.AuthorizationNumber,
                    authorization.Situation,
                    authorization.TypeOfService,
                    authorization.Notify,
                    authorization.AuthorizationDate,
                    authorization.Register);

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<AuthorizationsController>/CreateOnDemand
        [HttpPost("CreateOnDemand")]
        public async Task<IActionResult> CreateOnDemand([FromBody] List<AuthorizationViewModel> listAuthorizationViewModel)
        {
            try
            {
                var command = new AddAuthorizationOnDemandCommand(listAuthorizationViewModel);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<AuthorizationsController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AuthorizationViewModel authorization)
        {
            try
            {
                var command = new UpdateAuthorizationCommand(
                    id,
                    authorization.UserId,
                    authorization.EventId,
                    authorization.BudgetProductId,
                    authorization.BorrowerPersonId,
                    authorization.AuthorizationNumber,
                    authorization.Situation,
                    authorization.TypeOfService,
                    authorization.Notify,
                    authorization.AuthorizationDate,
                    authorization.Register);

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


        // DELETE api/<AuthorizationsController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteAuthorizationCommand(id);
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
