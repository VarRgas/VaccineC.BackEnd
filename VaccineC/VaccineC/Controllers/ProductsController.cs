using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Product;
using VaccineC.Query.Application.Queries.Product;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetProductListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{name}/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var command = new GetProductByNameQuery(name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetProductByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet, Route("GetAllVaccinesAutocomplete")]
        public async Task<IActionResult> GetAllVaccinesAutocomplete()
        {
            var command = new GetProductListVaccinesAutocompleteQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductViewModel product)
        {
            try
            {
                var command = new AddProductCommand(
                    product.ID,
                    product.SbimVaccinesId,
                    product.Situation,
                    product.Details,
                    product.SaleValue,
                    product.Register,
                    product.Name,
                    product.MinimumStock
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductViewModel product)
        {
            try
            {
                var command = new UpdateProductCommand(
                    id,
                    product.SbimVaccinesId,
                    product.Situation,
                    product.Details,
                    product.SaleValue,
                    product.Register,
                    product.Name,
                    product.MinimumStock
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteProductCommand(id);
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
