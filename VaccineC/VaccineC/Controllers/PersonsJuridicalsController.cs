
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.PersonJuridical;
using VaccineC.Query.Application.Queries.PersonJuridical;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsJuridicalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsJuridicalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPersonJuridicalListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personId}/GetPersonJuridicalByPersonId")]
        public async Task<IActionResult> GetPersonJuridicalByPersonId(Guid personId)
        {
            var command = new GetPersonJuridicalByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PersonsJuridicalViewModel juridical)
        {
            try
            {
                var command = new AddJuridicalComplementsCommand(
                    juridical.ID,
                    juridical.PersonID,
                    juridical.FantasyName,
                    juridical.CnpjNumber,
                    juridical.Register
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
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonsJuridicalViewModel juridical)
        {
            try
            {
                var command = new UpdateJuridicalComplementsCommand(
                    id,
                    juridical.PersonID,
                    juridical.FantasyName,
                    juridical.CnpjNumber,
                    juridical.Register
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
