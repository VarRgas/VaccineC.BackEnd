
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.PersonJuridical;

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
    }
}
