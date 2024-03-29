﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using VaccineC.Command.Application.Commands.Application;
using VaccineC.Query.Application.Queries.Application;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ApplicationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var command = new GetApplicationListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetApplicationByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetApplicationSipniIntegration")]
        public async Task<IActionResult> GetApplicationSipniIntegration()
        {
            try
            {
                var command = new GetApplicationSipniIntegrationQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{borrower}/{situation}/GetApplicationSipniIntegrationByParameter")]
        public async Task<IActionResult> GetApplicationSipniIntegrationByParameter(string borrower, string situation)
        {
            try
            {
                var command = new GetApplicationSipniIntegrationByParameterQuery(borrower, situation);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{personId}/GetPersonApplicationNumber")]
        public async Task<IActionResult> GetPersonApplicationNumber(Guid personId)
        {
            try
            {
                var command = new GetApplicationNumberByPersonIdQuery(personId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{personId}/{productId}/GetPersonApplicationProductSameDay")]
        public async Task<IActionResult> GetPersonApplicationProductSameDay(Guid personId, Guid productId)
        {
            try
            {
                var command = new GetPersonApplicationProductSameDayQuery(personId, productId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{applicationId}/GetSipniImunizationById")]
        public async Task<IActionResult> GetSipniImunizationById(Guid applicationId)
        {
            try
            {
                var command = new GetSipniImunizationByIdQuery(applicationId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{personId}/GetAvailableApplicationsByPersonId")]
        public async Task<IActionResult> GetAvailableApplicationsByPersonId(Guid personId)
        {
            try
            {
                var command = new GetAvailableApplicationsByPersonIdQuery(personId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{personId}/GetHistoryApplicationsByPersonId")]
        public async Task<IActionResult> GetHistoryApplicationsByPersonId(Guid personId)
        {
            try
            {
                var command = new GetHistoryApplicationsByPersonIdQuery(personId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{applicationId}/{userId}/VerifyApplicationAbleUpdate")]
        public async Task<IActionResult> VerifyApplicationAbleUpdate(Guid applicationId, Guid userId)
        {
            try
            {
                var command = new VerifyApplicationAbleUpdateQuery(applicationId, userId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{personName}/{responsibleId}/{applicationDate}/GetApplicationsByParameters")]
        public async Task<IActionResult> GetApplicationsByParameters(string personName, Guid responsibleId, DateTime applicationDate)
        {
            try
            {
                var command = new GetApplicationsByParametersQuery(responsibleId, applicationDate, personName);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{month}/{year}/GetApplicationsByPersonGender")]
        public async Task<IActionResult> GetApplicationsByPersonGender(int month, int year)
        {
            try
            {
                var command = new GetApplicationsByPersonGenderQuery(month, year);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{month}/{year}/GetApplicationsByProductId")]
        public async Task<IActionResult> GetApplicationsByProductId(int month, int year)
        {
            try
            {
                var command = new GetApplicationsByProductIdQuery(month, year);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{month}/{year}/GetApplicationsByAge")]
        public async Task<IActionResult> GetApplicationsByAge(int month, int year)
        {
            try
            {
                var command = new GetApplicationsByAgeQuery(month, year);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{month}/{year}/GetApplicationsByType")]
        public async Task<IActionResult> GetApplicationsByType(int month, int year)
        {
            try
            {
                var command = new GetApplicationsByTypeQuery(month, year);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{month}/{year}/GetSipniIntegrationSituation")]
        public async Task<IActionResult> GetSipniIntegrationSituation(int month, int year)
        {
            try
            {
                var command = new GetSipniIntegrationSituationQuery(month, year);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{month}/{year}/GetApplicationNumbers")]
        public async Task<IActionResult> GetApplicationNumbers(int month, int year)
        {
            try
            {
                var command = new GetApplicationNumbersQuery(month, year);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ApplicationViewModel application)
        {
            try
            {
                var command = new AddApplicationCommand(
                    application.ID,
                    application.UserId,
                    application.BudgetProductId,
                    application.ApplicationDate,
                    application.DoseType,
                    application.RouteOfAdministration,
                    application.ApplicationPlace,
                    application.Register,
                    application.ProductSummaryBatchId,
                    application.AuthorizationId,
                    application.SipniIntegrationId);

                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{applicationId}/{personId}/{userId}/AddSipniImunizationById")]
        public async Task<IActionResult> AddSipniImunizationById(Guid applicationId, Guid personId, Guid userId)
        {
            try
            {
                var command = new AddSipniImunizationByIdCommand(
                    applicationId,
                    personId,
                    userId);

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
