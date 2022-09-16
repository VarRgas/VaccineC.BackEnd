using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.MovementProduct;
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

        // POST api/<MovementsProductsController>/Create
        [HttpPost("{movementType}/Create")]
        public async Task<IActionResult> Create([FromBody] MovementProductViewModel movementProduct, string movementType)
        {
            try
            {
                var command = new AddMovementProductCommand(
                    movementProduct.ID,
                    movementProduct.MovementId,
                    movementProduct.ProductsId,
                    movementProduct.Batch,
                    movementProduct.UnitsNumber,
                    movementProduct.UnitaryValue,
                    movementProduct.Amount,
                    movementProduct.Details,
                    movementProduct.Register,
                    movementProduct.BatchManufacturingDate,
                    movementProduct.BatchExpirationDate,
                    movementProduct.Manufacturer,
                    movementType
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<MovementsProductsController>/3/Update
        [HttpPut("{id}/{movementType}/Update")]
        public async Task<IActionResult> Update(Guid id, string movementType, [FromBody] MovementProductViewModel movementProduct)
        {
            try
            {
                var command = new UpdateMovementProductCommand(
                    id,
                    movementProduct.MovementId,
                    movementProduct.ProductsId,
                    movementProduct.Batch,
                    movementProduct.UnitsNumber,
                    movementProduct.UnitaryValue,
                    movementProduct.Amount,
                    movementProduct.Details,
                    movementProduct.Register,
                    movementProduct.BatchManufacturingDate,
                    movementProduct.BatchExpirationDate,
                    movementProduct.Manufacturer,
                    movementType
                ); var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        // DELETE api/<MovementsProductsController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteMovementProductCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
