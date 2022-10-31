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

        // GET api/<AuthorizationsController>/GetAuthorizationByEventId
        [HttpGet("{eventId}/GetAuthorizationByEventId")]
        public async Task<IActionResult> getAuthorizationByEventId(Guid eventId)
        {
            var command = new GetAuthorizationByEventIdQuery(eventId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{month}/{year}/GetAuthorizationsDashInfo")]
        public async Task<IActionResult> GetAuthorizationsDashInfo(int month, int year)
        {
            try
            {
                var command = new GetAuthorizationsDashInfoQuery(month, year);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<AuthorizationsController>/GetAuthorizationByParameter
        [HttpGet("{information}/{situation}/{responsibleId}/GetAuthorizationByParameter")]
        public async Task<IActionResult> getAuthorizationByParameter(string information, string situation, Guid responsibleId)
        {
            try
            {
                var command = new GetAuthorizationByParameterQuery(information, situation, responsibleId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<AuthorizationsController>/GetAuthorizationsForApplication
        [HttpGet("GetAuthorizationsForApplication")]
        public async Task<IActionResult> getAuthorizationsForApplication()
        {
            var command = new GetAuthorizationForApplicationQuery();
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

        // POST api/<AuthorizationsController>/SuggestDoses
        [HttpPost("SuggestDoses")]
        public async Task<IActionResult> SuggestDoses([FromBody] List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {
            try
            {
                var command = new SuggestDosesCommand(listAuthorizationSuggestionViewModel);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<AuthorizationsController>/SuggestJuridicalDoses
        [HttpPost("SuggestJuridicalDoses")]
        public async Task<IActionResult> SuggestJuridicalDoses([FromBody] List<AuthorizationSuggestionViewModel> listAuthorizationSuggestionViewModel)
        {
            try
            {
                var command = new SuggestJuridicalDosesCommand(listAuthorizationSuggestionViewModel);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] AuthorizationUpdateViewModel authorization)
        {
            try
            {
                var command = new UpdateAuthorizationCommand(
                    id,
                    authorization.UserId,
                    authorization.EventId,
                    authorization.Register,
                    authorization.StartDateEvent,
                    authorization.StartTimeEvent);

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


        // DELETE api/<AuthorizationsController>/3/4/Delete
        [HttpDelete("{id}/{userId}/Delete")]
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            try
            {
                var command = new DeleteAuthorizationCommand(id, userId);
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
