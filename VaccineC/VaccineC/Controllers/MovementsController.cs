using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Queries.Movement;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovementsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<MovementsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetMovementListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<MovementsController>/5/GetAllByMovementNumber
        [HttpGet("{info}/GetAllByMovementNumber")]
        public async Task<IActionResult> GetByName(string info)
        {
            var command = new GetMovementListByMovementNumberQuery(info);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<MovementsController>/5/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetMovementByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
