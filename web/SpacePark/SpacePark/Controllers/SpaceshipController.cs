using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using AutoMapper;
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
        //private readonly IMapper _mapper;

        public SpaceshipController(ISpaceshipRepository repository) //, IMapper mapper
        {
            _spaceshipRepository = repository;
            //_mapper = mapper;
        }

        [HttpGet(Name = "GetAllSpaceships")]
        public async Task<ActionResult<IList<Parkinglot>>> GetAllSpaceshipsAsync()
        {
            try
            {
                var result = await _spaceshipRepository.GetAllSpaceshipsAsync();
                //var mappedResults = _mapper.Map<IList<Spaceship>>(result);
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
