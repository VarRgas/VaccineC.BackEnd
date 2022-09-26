using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.CompanySchedule;
using VaccineC.Query.Application.Queries.CompanySchedule;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CompaniesSchedulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompaniesSchedulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<CompaniesSchedulesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new GetCompanyScheduleListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET: api/<CompaniesSchedulesController>/5/GetAllCompaniesSchedulesByCompanyID
        [HttpGet("{companyId}/GetAllCompaniesSchedulesByCompanyID")]
        public async Task<IActionResult> GetAllCompaniesSchedulesByCompanyID(Guid companyId)
        {
            var command = new GetCompaniesSchedulesByCompanyIdQuery(companyId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // GET api/<CompaniesSchedulesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetCompanyScheduleByIdQuery(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // POST api/<ResourcesController>/CreateOnDemand
        [HttpPost("CreateOnDemand")]
        public async Task<IActionResult> CreateOnDemand([FromBody] List<CompanyScheduleViewModel> listCompanyScheduleViewModel)
        {
            try
            {
                var command = new AddCompanyScheduleOnDemandCommand(listCompanyScheduleViewModel);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CompaniesSchedulesController>/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CompanyScheduleViewModel companySchedule)
        {
            try
            {
                var command = new UpdateCompanyScheduleCommand(
                    id, 
                    companySchedule.CompanyId, 
                    companySchedule.Day, 
                    companySchedule.StartTime, 
                    companySchedule.FinalTime, 
                    companySchedule.Register
                );

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
            var command = new DeleteCompanyScheduleCommand(id);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
