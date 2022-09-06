using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Resource;
using VaccineC.Query.Application.Queries.Resource;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ResourcesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ResourcesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetResourceListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<ResourcesController>/Info/GetByName
        [HttpGet("{name}/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var command = new GetResourceByNameQuery(name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        // GET: api/<ResourcesController>/Info/GetByUser
        [HttpGet("{userId}/GetByUser")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var command = new GetResourceListByUserResourceQuery(userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<ResourcesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetResourceByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<ResourcesController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ResourceViewModel resource)
        {
            try
            {
                var command = new AddResourceCommand(resource.ID, resource.Name, resource.UrlName, resource.Register);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ResourcesController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ResourceViewModel resource)
        {
            try
            {
                var command = new UpdateResourceCommand(id, resource.Name, resource.UrlName, resource.Register);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        // DELETE api/<ResourcesController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeleteResourceCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Existem informações vinculadas a este recurso que impedem sua exclusão.");
            }
   
        }
    }
}
