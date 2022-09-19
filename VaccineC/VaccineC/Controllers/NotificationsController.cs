using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var command = new GetNotificationListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<NotificationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetNotificationByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<NotificationsController>/5/GetAllNotificationsByUser
        [HttpGet("{userId}/GetAllNotificationsByUser")]
        public async Task<IActionResult> GetAllNotificationsByUser(Guid userId)
        {
            var command = new GetNotificationByUserIdQuery(userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

    }
}
