using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.Company;
using VaccineC.Query.Application.Queries.Company;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CompaniesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetCompanyListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{name}/GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var command = new GetCompanyByNameQuery(name);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetCompanyByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}/GetCompaniesParametersByCompanyID")]
        public async Task<IActionResult> GetCompaniesParametersByCompanyID(Guid id)
        {
            var command = new GetCompaniesParametersByCompanyIDQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result); //amanda
        }


        [HttpGet("GetCompanyConfig")]
        public async Task<IActionResult> GetCompanyConfig()
        {
            try
            {
                var command = new GetCompanyConfigForAuthorizationQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CompanyViewModel company)
        {
            try
            {
                var command = new AddCompanyCommand(company.ID, company.PersonId, company.Details, company.Register);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CompanyViewModel company)
        {
            try
            {
                var command = new UpdateCompanyCommand(id, company.PersonId, company.Details, company.Register);
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
                var command = new DeleteCompanyCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateException ex)
            {
                return BadRequest("Existem informações vinculadas a esta empresa que impedem sua exclusão.");
            }
        }
    }
}
