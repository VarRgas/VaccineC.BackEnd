using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Movement;
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

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MovementViewModel movement)
        {
            try
            {
                var command = new AddMovementCommand(
                    movement.ID,
                    movement.MovementNumber,
                    movement.UsersId,
                    movement.MovementType,
                    movement.ProductsAmount,
                    movement.Register,
                    movement.Situation
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/FinishMovement")]
        public async Task<IActionResult> FinishMovement(Guid id, [FromBody] MovementViewModel movement)
        {
            try
            {
                var command = new FinishMovementCommand(
                    id,
                    movement.MovementNumber,
                    movement.UsersId,
                    movement.MovementType,
                    movement.ProductsAmount,
                    movement.Register,
                    movement.Situation
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        [HttpPut("{id}/CancelMovement")]
        public async Task<IActionResult> CancelMovement(Guid id, [FromBody] MovementViewModel movement)
        {
            try
            {
                var command = new CancelMovementCommand(
                    id,
                    movement.MovementNumber,
                    movement.UsersId,
                    movement.MovementType,
                    movement.ProductsAmount,
                    movement.Register,
                    movement.Situation
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }
    }
}
