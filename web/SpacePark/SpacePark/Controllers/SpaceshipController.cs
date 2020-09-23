using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using SpacePark.Services;

namespace SpacePark.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class SpaceshipController : ControllerBase
    {
        private readonly ISpaceshipRepository _spaceshipRepository;

        public SpaceshipController(ISpaceshipRepository repository)
        {
            _spaceshipRepository = repository;
        }

        [HttpGet("searchperson", Name = "GetSpaceshipsByPersonName")]
        public async Task<ActionResult<IList<Spaceship>>> GetSpaceshipByPersonName(string name)
        {
            try
            {
                var result = await _spaceshipRepository.GetSpaceshipByPersonNameAsync(name);
                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        public async Task<ActionResult<Spaceship>> GetSpaceshipByNameAsync([FromQuery] string name)
        {
            try
            {
                var result = await _spaceshipRepository.GetSpaceshipByNameAsync(name);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpDelete("{id}", Name = "CheckoutSpaceship")]
        public async Task<ActionResult<bool>> CheckoutSpaceship(int id)
        {
            try
            {
                var result = await _spaceshipRepository.CheckOutBySpaceshipId(id);
                if (result == false) return NotFound(result);
                return Ok();
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpPost("parkspaceship", Name = "PostParkShipByName")]
        public async Task<ActionResult<Spaceship>> ParkShipbyNameAsync(Spaceship spaceship)
        {
            try
            {
                var result = await _spaceshipRepository.ParkShipByNameAsync(spaceship);
                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }
        [HttpPost("postspaceship", Name = "PostSpaceship")]
        public async Task<ActionResult<Spaceship>> PostSpaceship(Spaceship spaceship)
        {
            
            
                try
                {
                    await _spaceshipRepository.Add(spaceship);

                    if (await _spaceshipRepository.Save())
                    {
                        return CreatedAtAction(nameof(GetSpaceshipByNameAsync), new { name = spaceship.Name }, spaceship);
                    }
                }
                catch (Exception e)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
                }
            return BadRequest();
        }

        [HttpGet(Name = "GetSpaceshipByName")]
        public async Task<ActionResult<Person>> GetPersonByNameAsync([FromQuery] string name)
        {
            try
            {
                var result = await _spaceshipRepository.GetSpaceshipByNameAsync(name);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }


        [HttpGet("{id}", Name = "GetSpaceshipById")]
        public async Task<ActionResult<Spaceship>> GetSpaceshipById(int id)
        {
            try
            {
                var result = await _spaceshipRepository.GetSpaceshipById(id);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpGet("GetSpaceShips", Name = "GetSwapiSpaceships")]
        public ActionResult<List<Spaceship>> GetSwapiSpaceships(string name)
        {
            var response = ParkingEngine.GetPersonData(($"people/?search={name}"));
            var foundPerson = response.Data.Results.FirstOrDefault(p => p.Name == name);

            if (foundPerson != null && foundPerson.Starships != null)
            {
                Person.AddSpaceshipsToPerson(foundPerson);
                return foundPerson.Spaceships;
            }

            return null;
        }


    }
}
