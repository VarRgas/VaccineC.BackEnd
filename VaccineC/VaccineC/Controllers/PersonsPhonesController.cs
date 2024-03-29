﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccineC.Command.Application.Commands.PersonPhone;
using VaccineC.Query.Application.Queries.PersonPhone;
using VaccineC.Query.Application.ViewModels;

namespace VaccineC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonsPhonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonsPhonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<PersonsPhonesController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var command = new GetPersonPhoneListQuery();
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PersonsPhonesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var command = new GetPersonPhoneByIdQuery(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<PersonsPhonesController>/5/GetAllPersonsPhonesByPersonId
        [HttpGet("{personId}/GetAllPersonsPhonesByPersonId")]
        public async Task<IActionResult> GetAllCompaniesSchedulesByCompanyID(Guid personId)
        {
            try
            {
                var command = new GetPersonsPhonesByPersonIdQuery(personId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/<PersonsPhonesController>/5/GetAllPersonsPhonesCellByPersonId
        [HttpGet("{personId}/GetAllPersonsPhonesCellByPersonId")]
        public async Task<IActionResult> GetAllPersonsPhonesCellByPersonId(Guid personId)
        {
            try
            {
                var command = new GetPersonPhoneCelListQuery(personId);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PersonsPhonesController>/Create
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] PersonPhoneViewModel personPhone)
        {
            try
            {
                var command = new AddPersonPhoneCommand(
                    personPhone.ID,
                    personPhone.PersonID,
                    personPhone.PhoneType,
                    personPhone.NumberPhone,
                    personPhone.CodeArea,
                    personPhone.Register
                );
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PersonsPhonesController>/3/Update
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonPhoneViewModel personPhone)
        {
            try
            {
                var command = new UpdatePersonPhoneCommand(
                    id,
                    personPhone.PersonID,
                    personPhone.PhoneType,
                    personPhone.NumberPhone,
                    personPhone.CodeArea,
                    personPhone.Register
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

        // DELETE api/<PersonsPhonesController>/3/Delete
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var command = new DeletePersonPhoneCommand(id);
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{personId}/GetPrincipalPersonPhone")]
        public async Task<IActionResult> GetPrincipalPersonPhone(Guid personId)
        {
            try
            {
                var command = new GetPrincipalPersonPhoneByPersonIdQuery(personId);
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
