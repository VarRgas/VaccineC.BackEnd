using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.UserCommands;
using VaccineC.Query.Application.Queries.Person;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers

{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PersonsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPersonListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<PersonsController>/Info/GetAllByType
        [HttpGet("{personType}/GetAllByType")]
        public async Task<IActionResult> GetAllByType(string personType)
        {
            var command = new GetPersonListByTypeQuery(personType);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
