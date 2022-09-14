using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.ProductDoses;
using VaccineC.Query.Application.Queries.ProductDoses;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsDosesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsDosesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetProductDosesListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{productsId}/GetAllByProductId")]
        public async Task<IActionResult> GetProductsDosesByProductId(Guid productsId)
        {
            var command = new GetProductsDosesByProductIdQuery(productsId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetProductDosesByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductDosesViewModel productDoses)
        {
            try
            {
                var command = new AddProductDosesCommand(
                    productDoses.ID,
                    productDoses.ProductsId,
                    productDoses.DoseType,
                    productDoses.DoseRangeMonth,
                    productDoses.Register
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
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDosesViewModel productDose)
        {
            try
            {
                var command = new UpdateProductDosesCommand(
                    id,
                    productDose.ProductsId,
                    productDose.DoseType,
                    productDose.DoseRangeMonth,
                    productDose.Register
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
                var command = new DeleteProductDosesCommand(id);
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
