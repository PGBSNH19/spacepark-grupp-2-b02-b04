using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using SpacePark.Services;

namespace SpacePark.Controllers
{
    [Route("api/v1.0/[controller]")]
    [ApiController]
    public class ParkinglotController : ControllerBase
    {

        private readonly IParkinglotRepository _repository;

        public ParkinglotController(IParkinglotRepository repository)
        {
            _repository = repository;
        }

        [HttpPost(Name = "CreateParking")]
        public async Task<IActionResult> CreateActor(Parkinglot person)
        {
            try
            {

                var actorFromRepo = await _repository.Add(actor);
                if (actorFromRepo != null)
                {
                    return CreatedAtAction(nameof(GetActorById), new { id = actor.Id }, actor);
                }
                return BadRequest("Failed to create actor.");
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Failed to create the actor. Exception thrown when attempting to add data to the database: {e.Message}");
            }
        }
    }
