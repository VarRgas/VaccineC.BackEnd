using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.PersonAddress;
using VaccineC.Query.Application.Queries.PersonAddress;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonsAddressesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsAddressesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PersonsAddressesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPersonAddressListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<PersonsAddressesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetPersonAddressByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<PersonsAddressesController>/5/GetAllPersonsAddressesByPersonId
        [HttpGet("{personId}/GetAllPersonsAddressesByPersonId")]
        public async Task<IActionResult> GetAllCompaniesSchedulesByCompanyID(Guid personId)
        {
            var command = new GetPersonsAddressesByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<PersonsAddressesController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PersonAddressViewModel personAddress)
        {
            try
            {
                var command = new AddPersonAddressComand(
                    personAddress.ID,
                    personAddress.PersonID,
                    personAddress.AddressType,
                    personAddress.PublicPlace,
                    personAddress.District,
                    personAddress.AddressNumber,
                    personAddress.Complement,
                    personAddress.AddressCode,
                    personAddress.ReferencePoint,
                    personAddress.City,
                    personAddress.State,
                    personAddress.Country,
                    personAddress.Register
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PersonsAddressesController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonAddressViewModel personAddress)
        {
            try
            {
                var command = new UpdatePersonAddressCommand(
                    id,
                    personAddress.PersonID,
                    personAddress.AddressType,
                    personAddress.PublicPlace,
                    personAddress.District,
                    personAddress.AddressNumber,
                    personAddress.Complement,
                    personAddress.AddressCode,
                    personAddress.ReferencePoint,
                    personAddress.City,
                    personAddress.State,
                    personAddress.Country,
                    personAddress.Register
                 );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        // DELETE api/<PersonsAddressesController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePersonAddressCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{personId}/GetPrincipalPersonAddress")]
        public async Task<IActionResult> GetPrincipalPersonAddress(Guid personId)
        {
            var command = new GetPrincipalPersonAddressByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
