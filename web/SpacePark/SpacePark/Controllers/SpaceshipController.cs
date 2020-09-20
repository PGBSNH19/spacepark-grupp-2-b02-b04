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

        [HttpGet(Name = "GetAllSpaceships")]
        public async Task<ActionResult<IList<Parkinglot>>> GetAllSpaceshipsAsync()
        {
            try
            {
                var result = await _spaceshipRepository.GetAllSpaceshipsAsync();
                if (result.Count == 0) return NotFound(result);
                return Ok(result);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpGet(Name = "GetSpaceshipsByPersonName")]
        public async Task<ActionResult<IList<Spaceship>>> GetSpaceshipsByPersonName(Person person)
        {
            try
            {
                var result = await _spaceshipRepository.GetAllSpaceshipsByPersonName(person);
                if (result.Count == 0) return NotFound(result);
                return Ok(result);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpGet(Name = "ParkShip")]
        public async Task<ActionResult<IList<Spaceship>>> ParkShip(int spaceshipID, bool loggedIn = true)
        {
            try
            {
                var result = await _spaceshipRepository.GetAllSpaceshipsByPersonName(person);
                if (result.Count == 0) return NotFound(result);
                return Ok(result);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }
    }
}
