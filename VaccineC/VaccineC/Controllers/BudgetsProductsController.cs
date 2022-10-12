using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.BudgetProduct;
using VaccineC.Query.Application.Queries.BudgetProduct;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BudgetsProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetsProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<BudgetsProductsController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetBudgetProductListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<BudgetsProductsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetBudgetProductByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<BudgetsProductsController>/5/GetBudgetsProductsByBudget
        [HttpGet("{budgetId}/GetBudgetsProductsByBudget")]
        public async Task<IActionResult> GetAllBudgetsProductsByBudgetID(Guid budgetId)
        {
            var command = new GetBudgetProductListByBudgetQuery(budgetId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<BudgetsProductsController>/5/5/GetAllPendingBudgetsProductsByBorrower
        [HttpGet("{budgetId}/{borrowerId}/{startDate}/GetAllPendingBudgetsProductsByBorrower")]
        public async Task<IActionResult> GetAllPendingBudgetsProductsByBorrower(Guid budgetId, Guid borrowerId, DateTime startDate)
        {
            var command = new GetPendingBudgetProductListByBorrowerQuery(budgetId, borrowerId, startDate);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<BudgetsProductsController>/5/GetAllPendingBudgetsProductsByResponsible
        [HttpGet("{budgetId}/GetAllPendingBudgetsProductsByResponsible")]
        public async Task<IActionResult> GetAllPendingBudgetsProductsByResponsible(Guid budgetId)
        {
            var command = new GetPendingBudgetProductListByResponsibleQuery(budgetId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<BudgetsProductsController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] BudgetProductViewModel budgetProductViewModel)
        {
            try
            {
                var command = new AddBudgetProductCommand(
                    budgetProductViewModel.ID,
                    budgetProductViewModel.BudgetId,
                    budgetProductViewModel.ProductId,
                    budgetProductViewModel.BorrowerPersonId,
                    budgetProductViewModel.ProductDose,
                    budgetProductViewModel.Details,
                    budgetProductViewModel.EstimatedSalesValue,
                    budgetProductViewModel.SituationProduct,
                    budgetProductViewModel.Register

                    );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BudgetsProductsController>/CreateOnDemand
        [HttpPost("CreateOnDemand")]
        public async Task<IActionResult> CreateOnDemand([FromBody] List<BudgetProductViewModel> listBudgetProductViewModel)
        {
            try
            {
                var command = new AddBudgetProductOnDemandCommand(listBudgetProductViewModel);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<BudgetsProductsController>/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] BudgetProductViewModel budgetProductViewModel)
        {
            try
            {
                var command = new UpdateBudgetProductCommand(
                    id,
                    budgetProductViewModel.BudgetId,
                    budgetProductViewModel.ProductId,
                    budgetProductViewModel.BorrowerPersonId,
                    budgetProductViewModel.ProductDose,
                    budgetProductViewModel.Details,
                    budgetProductViewModel.EstimatedSalesValue,
                    budgetProductViewModel.SituationProduct,
                    budgetProductViewModel.Register,
                    budgetProductViewModel.UserId
                );

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<BudgetsProductsController>/5/Delete
        [HttpDelete("{id}/{userId}/Delete")]
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            try
            {
                var command = new DeleteBudgetProductCommand(id, userId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<BudgetsProductsController>/CreateOnDemand
        [HttpPost("{id}/{numberOfTimes}/{repeatBorrower}/{userId}/RepeatOnDemand")]
        public async Task<IActionResult> RepeatOnDemand(Guid id, int numberOfTimes, Boolean repeatBorrower, Guid? userId)
        {
            try
            {
                var command = new RepeatBudgetProductOnDemandCommand(numberOfTimes, id, repeatBorrower, userId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
