using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Person;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPersonListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personType}/GetAllByType")]
        public async Task<IActionResult> GetAllByType(string personType)
        {
            var command = new GetPersonListByTypeQuery(personType);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{name}/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var command = new GetPersonListByNameQuery(name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetPersonByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PersonViewModel person)
        {
            try
            {
                var command = new AddPersonCommand(
                    person.ID,
                    person.PersonType,
                    person.Name,
                    person.CommemorativeDate,
                    person.Email,
                    person.ProfilePic,
                    person.Details,
                    person.Register
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
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonViewModel person)
        {
            try
            {
                var command = new UpdatePersonCommand(
                    id,
                    person.PersonType,
                    person.Name,
                    person.CommemorativeDate,
                    person.Email,
                    person.ProfilePic,
                    person.Details,
                    person.Register
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePersonCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet, Route("GetAllUserAutocomplete")]
        public async Task<IActionResult> GetAllUserAutocomplete()
        {
            var command = new GetPersonListUserAutocompleteQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet, Route("GetAllCompanyAutocomplete")]
        public async Task<IActionResult> GetAllCompanyAutocomplete()
        {
            var command = new GetPersonListCompanyAutocompleteQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
