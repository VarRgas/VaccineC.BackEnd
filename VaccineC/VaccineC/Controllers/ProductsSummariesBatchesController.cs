using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    }
}
