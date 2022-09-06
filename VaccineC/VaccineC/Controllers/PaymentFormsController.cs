using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.PaymentForm;
using VaccineC.Query.Application.Queries.PaymentForm;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PaymentFormsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentFormsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PaymentFormsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetPaymentFormListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<PaymentFormsController>/Info/GetByName
        [HttpGet("{name}/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var command = new GetPaymentFormByNameQuery(name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<PaymentFormsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetPaymentFormByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<PaymentFormsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PaymentFormViewModel paymentForm)
        {
            try
            {
                var command = new AddPaymentFormCommand(paymentForm.ID, paymentForm.Name, paymentForm.MaximumInstallments, paymentForm.Register);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PaymentFormsController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PaymentFormViewModel paymentForm)
        {
            try
            {
                var command = new UpdatePaymentFormCommand(id, paymentForm.Name, paymentForm.MaximumInstallments, paymentForm.Register);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }

        // DELETE api/<PaymentFormsController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeletePaymentFormCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Existem informações vinculadas a esta forma de pagamento que impedem sua exclusão.");
            }
        }

    }
}
