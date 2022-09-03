using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
