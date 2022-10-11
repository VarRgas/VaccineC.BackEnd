using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.AuthorizationNotification;
using VaccineC.Query.Application.Queries.AuthorizationNotification;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationsNotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationsNotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthorizationsNotificationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAuthorizationNotificationListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<AuthorizationsNotificationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetAuthorizationNotificationByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<AuthorizationsNotificationsController>/GetNotificationsByAuthorizationId
        [HttpGet("{authorizationId}/GetNotificationsByAuthorizationId")]
        public async Task<IActionResult> GetNotificationsByAuthorizationId(Guid authorizationId)
        {
            var command = new GetAuthorizationNotificationByAuthorizationIdQuery(authorizationId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        // POST api/<AuthorizationsNotificationsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] AuthorizationNotificationViewModel authorizationNotification)
        {
            try
            {
                var command = new AddAuthorizationNotificationCommand(
                    authorizationNotification.ID,
                    authorizationNotification.AuthorizationId,
                    authorizationNotification.EventId,
                    authorizationNotification.PersonPhone,
                    authorizationNotification.Message,
                    authorizationNotification.SendDate,
                    authorizationNotification.SendHour,
                    authorizationNotification.Register);

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
