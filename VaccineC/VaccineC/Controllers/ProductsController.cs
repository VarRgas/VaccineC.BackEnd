using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Query.Application.Queries.Product;

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

        [HttpGet, Route("GetAllCompanyAutocomplete")]
        public async Task<IActionResult> GetAllCompanyAutocomplete()
        {
            var command = new GetProductListVaccinesAutocompleteQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
