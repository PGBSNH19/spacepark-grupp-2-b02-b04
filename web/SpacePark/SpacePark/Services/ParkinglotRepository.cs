using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<Parkinglot>> GetAllParkinglotsAsync()
        {
            _logger.LogInformation($"Getting all available parkinglots.");
            return await _context.Parkinglot.ToListAsync();
        }

        public async Task<Parkinglot> GetParkinglotByIdAsync(int id)
        {
            _logger.LogInformation($"Getting parkinglot with id {id}");
            return await _context.Parkinglot.SingleOrDefaultAsync(x => x.ParkinglotID == id); ;
        }
        public async Task ClearParkedShip(Spaceship spaceShip)
        {
            await using var context = new SpaceParkContext();

            // Finds the ship in the person table and set it to null.
            context.Parkinglot.Where(x => x.SpaceshipID == spaceShip.SpaceshipID)
                .FirstOrDefault()
                .Spaceship = null;

            context.SaveChanges();
        }

    }
}
