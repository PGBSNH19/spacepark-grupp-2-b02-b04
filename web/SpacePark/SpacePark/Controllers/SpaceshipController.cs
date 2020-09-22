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

        [HttpPost(Name = "PostCheckOutBySpaceshipName")]
        public async Task<ActionResult<Spaceship>> PostCheckOutBySpaceshipName(string name)
        {
            try
            {
                var result = await _spaceshipRepository.CheckOutByNameAsync(name);
                if (result == null) return NotFound(result);
                return Ok(result);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpPost(Name = "PostParkShipByName")]
        public async Task<ActionResult<Spaceship>> ParkShipbyNameAsync(string shipName)
        {
            try
            {
                var result = await _spaceshipRepository.ParkShipByNameAsync(shipName);
                if (result == null) return NotFound(result);
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
