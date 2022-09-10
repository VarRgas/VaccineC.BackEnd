using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.ProductDoses;

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
    }
}
