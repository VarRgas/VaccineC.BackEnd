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
            try
            {
                var command = new GetPersonListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{personType}/GetAllByType")]
        public async Task<IActionResult> GetAllByType(string personType)
        {
            try
            {
                var command = new GetPersonListByTypeQuery(personType);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{name}/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            try
            {
                var command = new GetPersonListByNameQuery(name);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetPersonByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeletePersonCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Existem informações vinculadas a esta pessoa que impedem sua exclusão.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAllUserAutocomplete")]
        public async Task<IActionResult> GetAllUserAutocomplete()
        {
            try
            {
                var command = new GetPersonListUserAutocompleteQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAllCompanyAutocomplete")]
        public async Task<IActionResult> GetAllCompanyAutocomplete()
        {
            try
            {
                var command = new GetPersonListCompanyAutocompleteQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Route("GetAllAuthorizationAutocomplete")]
        public async Task<IActionResult> GetAllAuthorizationAutocomplete()
        {
            try
            {
                var command = new GetPersonListAuthorizationAutocompleteQuery();
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
