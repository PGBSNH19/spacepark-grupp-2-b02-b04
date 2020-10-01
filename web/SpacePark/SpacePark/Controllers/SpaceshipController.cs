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

        public async Task<ActionResult<Spaceship>> GetSpaceshipByName([FromQuery] string name)
        {
            try
            {
                var result = await _spaceshipRepository.GetSpaceshipByName(name);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpDelete("{id}", Name = "CheckoutSpaceship")]
        public async Task<ActionResult<Spaceship>> CheckoutSpaceship(int id)
        {
            try
            {
                var result = await _spaceshipRepository.CheckOutBySpaceshipId(id);
                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpPost("parkspaceship", Name = "ParkShipByName")]
        public async Task<ActionResult<Spaceship>> ParkShipbyName(Spaceship spaceship)
        {
            try
            {
                var result = await _spaceshipRepository.ParkShipByName(spaceship);
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
                        return CreatedAtAction(nameof(GetSpaceshipByName), new { name = spaceship.Name }, spaceship);
                    }
                }
                catch (Exception e)
                {
                    return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database Failure: {e.Message}");
                }
            return BadRequest();
        }

        [HttpGet(Name = "GetSpaceshipByName")]
        public async Task<ActionResult<Person>> GetPersonByName([FromQuery] string name)
        {
            try
            {
                var result = await _spaceshipRepository.GetSpaceshipByName(name);
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
        public async Task<ActionResult<List<Spaceship>>> GetSwapiSpaceships(string name)
        {
            var response = await ParkingEngine.GetPersonData(($"people/?search={name}"));
            var foundPerson = response.FirstOrDefault(p => p.Name == name);

            if (foundPerson != null && foundPerson.Starships != null)
            {
                Person.AddSpaceshipsToPerson(foundPerson);
                return Ok(foundPerson.Spaceships);
            }

            return NotFound();
        }


    }
}
