using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Notification;
using VaccineC.Query.Application.Queries.Notification;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class NotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<NotificationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var command = new GetNotificationListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<NotificationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetNotificationByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<NotificationsController>/5/GetAllNotificationsByUser
        [HttpGet("{userId}/GetAllNotificationsByUser")]
        public async Task<IActionResult> GetAllNotificationsByUser(Guid userId)
        {
            try
            {
                var command = new GetNotificationByUserIdQuery(userId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ManageNotifications")]
        public async Task<IActionResult> getManageNotifications()
        {
            try
            {
                var command = new ManageNotificationsCommand();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<NotificationsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] NotificationViewModel notification)
        {
            try
            {
                var command = new AddNotificationCommand(notification.ID, notification.UserId, notification.Message, notification.MessageType, notification.Situation, notification.Register);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<NotificationsController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NotificationViewModel notification)
        {
            try
            {
                var command = new UpdateNotificationCommand(id, notification.UserId, notification.Message, notification.MessageType, notification.Situation, notification.Register);
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

        // DELETE api/<NotificationsController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteNotificationCommand(id);
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

        // POST api/<NotificationsController>/UpdateOnDemand
        [HttpPost("UpdateOnDemand")]
        public async Task<IActionResult> CreateOnDemand([FromBody] List<NotificationViewModel> listNotificationViewModel)
        {
            try
            {
                var command = new UpdateNotificationsOnDemandCommand(listNotificationViewModel);
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
