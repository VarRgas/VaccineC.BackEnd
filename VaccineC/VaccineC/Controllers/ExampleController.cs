using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Command.Application.Commands.Example;
using VaccineC.Query.Application.Queries.Example;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[exampleController]")]
    [ApiController]

    public class ExampleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExampleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<ExamplesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetExampleListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<ExamplesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var command = new GetExampleByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<ExamplesController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ExampleViewModel example)
        {
            var command = new AddExampleCommand(example.Name, example.Phone, example.CPF, example.Email, example.HasPending);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //// PUT api/<ExamplesController>/3/Update
        //[HttpPut("{id}/Update")]
        //public async Task<IActionResult> Update(int id, [FromBody] ExampleViewModel example)
        //{
        //    try
        //    {
        //        var command = new UpdateExampleCommand(id, example.Name, example.Phone, example.CPF, example.Email, example.HasPending, example.Timestamp);
        //        var result = await _mediator.Send(command);
        //        return Ok(result);
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        return Conflict(ex);
        //    }
        //}

        //// DELETE api/<ExamplesController>/3/Delete
        //[HttpDelete("{id}/Delete")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var command = new DeleteExampleCommand(id);
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}
    }
}
