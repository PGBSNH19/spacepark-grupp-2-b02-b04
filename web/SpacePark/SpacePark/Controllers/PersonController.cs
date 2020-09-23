using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using SpacePark.Services;

namespace SpacePark.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet(Name = "GetPersonByName")]
        public async Task<ActionResult<Person>> GetPersonByNameAsync([FromQuery] string name)
        {
            try
            {
                var result = await _personRepository.GetPersonByNameAsync(name);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(string name)
        {
            var person = _personRepository.CheckIn(name);
            if (person != null)
            {
                try
                {
                    await _personRepository.Add(person);

                    if (await _personRepository.Save())
                    {
                        return CreatedAtAction(nameof(GetPersonByNameAsync), new { name = person.Name }, person);
                    }
                }
                catch (Exception e)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
                }
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Person>> PutPerson(Person person)
        {
            if (person != null)
            {
                try
                {
                    await _personRepository.Update(person);

                    return CreatedAtAction(nameof(GetPersonByNameAsync), new { name = person.Name }, person);
                }
                catch (Exception e)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
                }
            }
            return BadRequest();
        }
    }
}
