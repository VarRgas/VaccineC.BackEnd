using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.ProductSummaryBatch;
using VaccineC.Query.Application.Queries.ProductSummaryBatch;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsSummariesBatchesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsSummariesBatchesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetProductSummaryBatchListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllBelowMinimumStock")]
        public async Task<IActionResult> getAllBatchsBelowMinimumStock()
        {
            var command = new GetProductSummaryBatchBelowMinimumStockListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllNotEmpty")]
        public async Task<IActionResult> getAllNotEmpty()
        {
            var command = new GetNotEmptyProductSummaryBatchListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetProductSummaryBatchByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{productsId}/GetAllByProductId")]
        public async Task<IActionResult> GetProductsDosesByProductId(Guid productsId)
        {
            var command = new GetProductSummaryBatchByProductIdQuery(productsId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{productsId}/GetValidProductsSummariesBatches")]
        public async Task<IActionResult> GetValidProductsSummariesBatches(Guid productsId)
        {
            var command = new GetValidProductSummaryBatchListQuery(productsId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{productId}/{name}/GetProductSummaryBatchByName")]
        public async Task<IActionResult> GetProductSummaryBatchByName(Guid productId, string name)
        {
            var command = new GetProductSummaryBatchByNameQuery(productId, name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductSummaryBatchViewModel summaryBatchViewModel)
        {
            try
            {
                var command = new AddProductSummaryBatchCommand(
                    summaryBatchViewModel.ID,
                    summaryBatchViewModel.Batch,
                    summaryBatchViewModel.NumberOfUnitsBatch,
                    summaryBatchViewModel.ManufacturingDate,
                    summaryBatchViewModel.ValidityBatchDate,
                    summaryBatchViewModel.Register,
                    summaryBatchViewModel.Manufacturer,
                    summaryBatchViewModel.ProductsId
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
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductSummaryBatchViewModel summaryBatchViewModel)
        {
            try
            {
                var command = new UpdateProductSummaryBatchCommand(
                    summaryBatchViewModel.ID,
                    summaryBatchViewModel.Batch,
                    summaryBatchViewModel.NumberOfUnitsBatch,
                    summaryBatchViewModel.ManufacturingDate,
                    summaryBatchViewModel.ValidityBatchDate,
                    summaryBatchViewModel.Register,
                    summaryBatchViewModel.Manufacturer,
                    summaryBatchViewModel.ProductsId
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
                var command = new DeleteProductSummaryBatchCommand(id);
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
