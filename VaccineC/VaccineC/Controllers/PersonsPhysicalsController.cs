using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.PersonPhone;
using VaccineC.Query.Application.Queries.PersonPhone;
using VaccineC.Query.Application.Queries.PersonPhysical;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsPhysicalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsPhysicalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PersonsPhysicalsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPersonPhysicalListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<PersonsPhysicalsController>/5/GetPersonPhysicalByPersonId
        [HttpGet("{personId}/GetPersonPhysicalByPersonId")]
        public async Task<IActionResult> GetPersonPhysicalByPersonId(Guid personId)
        {
            var command = new GetPersonPhysicalByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
