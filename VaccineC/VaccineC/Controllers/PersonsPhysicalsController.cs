using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.PersonPhysical;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPersonPhysicalListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personId}/GetPersonPhysicalByPersonId")]
        public async Task<IActionResult> GetPersonPhysicalByPersonId(Guid personId)
        {
            var command = new GetPersonPhysicalByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PersonsPhysicalViewModel physical)
        {
            try
            {
                var command = new AddPhysicalComplementsCommand(
                    physical.ID,
                    physical.PersonID,
                    physical.MaritalStatus,
                    physical.Gender,
                    physical.DeathDate,
                    physical.Register,
                    physical.CnsNumber,
                    physical.CpfNumber
                    );

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonsPhysicalViewModel physical)
        {
            try
            {
                var command = new UpdatePhysicalComplementsCommand(
                    id,
                    physical.PersonID,
                    physical.MaritalStatus,
                    physical.Gender,
                    physical.DeathDate,
                    physical.Register,
                    physical.CnsNumber,
                    physical.CpfNumber
                 );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }
    }
}
