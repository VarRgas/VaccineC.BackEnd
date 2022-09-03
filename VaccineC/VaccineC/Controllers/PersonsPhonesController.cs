using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Queries.PersonPhone;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonsPhonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsPhonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PersonsPhonesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPersonPhoneListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<PersonsPhonesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetPersonPhoneByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<PersonsPhonesController>/5/GetAllPersonsPhonesByPersonId
        [HttpGet("{personId}/GetAllPersonsPhonesByPersonId")]
        public async Task<IActionResult> GetAllCompaniesSchedulesByCompanyID(Guid personId)
        {
            var command = new GetPersonsPhonesByPersonIdQuery(personId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
