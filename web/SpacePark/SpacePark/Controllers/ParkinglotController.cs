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
    public class ParkinglotController : ControllerBase
    {

        private readonly IParkinglotRepository _parkinglotRepository;


        public ParkinglotController(IParkinglotRepository parkinglotRepository)
        {
            _parkinglotRepository = parkinglotRepository;
        }

  
        [HttpGet(Name = "GetAllParkinglots")]
        public async Task<ActionResult<IList<Parkinglot>>> GetAllParkinglotsAsync()
        {
            try
            {
                var result = await _parkinglotRepository.GetAllParkinglotsAsync();
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
