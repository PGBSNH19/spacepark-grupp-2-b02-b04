﻿using System;
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

    }
}
