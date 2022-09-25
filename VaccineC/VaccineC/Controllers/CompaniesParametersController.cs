using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.CompanyParameter;
using VaccineC.Query.Application.Queries.CompanyParameter;
using VaccineC.Query.Application.ViewModels;


namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CompaniesParametersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesParametersController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetCompanyParameterListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetCompanyParameterByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetDefaultCompanyParameter")]
        public async Task<IActionResult> GetDefaultCompanyParameter()
        {
            var command = new GetDefaultCompanyParametersQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CompaniesParametersViewModel companyParameter)
        {
            try
            {
                var command = new AddCompanyParameterCommand(
                    companyParameter.ID,
                    companyParameter.CompanyId,
                    companyParameter.ApplicationTimePerMinute,
                    companyParameter.MaximumDaysBudgetValidity,
                    companyParameter.Register,
                    companyParameter.ScheduleColor
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
        public async Task<IActionResult> Update(Guid id, [FromBody] CompaniesParametersViewModel companyParameter)
        {
            try
            {
                var command = new UpdateCompanyParameterCommand(
                    id,
                    companyParameter.CompanyId,
                    companyParameter.ApplicationTimePerMinute,
                    companyParameter.MaximumDaysBudgetValidity,
                    companyParameter.Register,
                    companyParameter.ScheduleColor
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Conflict(ex);
            }
        }
    }
}
