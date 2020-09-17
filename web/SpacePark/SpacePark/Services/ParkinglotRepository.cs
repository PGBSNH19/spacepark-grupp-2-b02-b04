﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class ParkinglotRepository : Repository, IParkinglotRepository
    {
        public ParkinglotRepository(SpaceParkContext context, ILogger<Parkinglot> logger) : base(context, logger)
        {
        }

        public async Task <IList<Parkinglot>> GetAllParkinglotsAsync()
        {

                var query = _context.Parkinglot;

                _logger.LogInformation($"Getting all available parkinglots.");

                return await query.ToListAsync();
        }

        public async Task <Parkinglot> GetParkinglotByIdAsync(int id)
        {
            _logger.LogInformation($"Getting parkinglot with id {id}");

            var query = await _context.Parkinglot
                .SingleOrDefaultAsync(x => x.ParkinglotID == id);
            return query;
        }

    }
}
