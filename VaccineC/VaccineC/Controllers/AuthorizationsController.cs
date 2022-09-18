using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Queries.Authorization;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthorizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<AuthorizationsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetAuthorizationListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<AuthorizationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetAuthorizationByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<AuthorizationsController>/GetSummarySituationAuthorization
        [HttpGet("GetSummarySituationAuthorization")]
        public async Task<IActionResult> getSummarySituationAuthorization()
        {
            var command = new GetSummarySituationAuthorizationQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
