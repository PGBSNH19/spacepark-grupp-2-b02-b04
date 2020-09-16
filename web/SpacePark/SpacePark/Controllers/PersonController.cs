﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        private readonly IMapper _mapper;

        public PersonController(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllPeople")]
        public async Task<ActionResult<IList<Person>>> GetAllPoepleAsync()
        {
            try
            {
                var result = await _personRepository.GetAllPeopleAsync();
                var mappedResults = _mapper.Map<IList<Person>>(result);
                if (result.Count == 0) return NotFound(result);
                return Ok(mappedResults);
            }
            catch (Exception e)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }

        [HttpGet(Name = "GetPersonByName")]
        public async Task<ActionResult<Person>> GetPersonByNameAsync([FromQuery] string name)
        {
            try
            {
                var result = await _personRepository.GetPersonByNameAsync(name);
                var mappedResults = _mapper.Map<IList<Person>>(result);
                if (result.Count == 0) return NotFound();
                return Ok(mappedResults);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Database failure: {e.Message}");
            }
        }





    }
}
