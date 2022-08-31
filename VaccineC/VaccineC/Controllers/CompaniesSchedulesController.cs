using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.CompanySchedule;

namespace VaccineC.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CompaniesSchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesSchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CompaniesSchedulesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetCompanyScheduleListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<CompaniesSchedulesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetCompanyScheduleByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
