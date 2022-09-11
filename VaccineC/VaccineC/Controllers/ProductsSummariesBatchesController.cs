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

        // GET: api/<ProductsSummariesBatchesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetProductSummaryBatchListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<ProductsSummariesBatchesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetProductSummaryBatchByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<ProductsSummariesBatchesController>/5/GetAllByProductId
        [HttpGet("{productsId}/GetAllByProductId")]
        public async Task<IActionResult> GetProductsDosesByProductId(Guid productsId)
        {
            var command = new GetProductSummaryBatchByProductIdQuery(productsId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<ProductsSummariesBatchesController>/Create
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

        // POST api/<ProductsSummariesBatchesController>/5/Update
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

        // POST api/<ProductsSummariesBatchesController>/5/Delete
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
