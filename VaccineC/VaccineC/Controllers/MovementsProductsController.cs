using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Query.Application.Queries.MovementProduct;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovementsProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovementsProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<MovementsProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetMovementProductListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<MovementsProductsController>/5/GetAllMovementsProductsByMovementId
        [HttpGet("{movementId}/GetAllMovementsProductsByMovementId")]
        public async Task<IActionResult> GetAllCompaniesSchedulesByCompanyID(Guid movementId)
        {
            var command = new GetMovementProductListByMovementQuery(movementId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        // GET: api/<MovementsProductsController>/5/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetMovementProductByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
