using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.UserCommands;
using VaccineC.Query.Application.Queries.User;
using VaccineC.Query.Application.Queries.UserResource;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<UsersResourcesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetUserResourceListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<UsersResourcesController>/Info/GetByUser
        [HttpGet("{usersId}/GetByUser")]
        public async Task<IActionResult> GetByUser(Guid usersId)
        {
            var command = new GetUserResourceListByUserQuery(usersId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
