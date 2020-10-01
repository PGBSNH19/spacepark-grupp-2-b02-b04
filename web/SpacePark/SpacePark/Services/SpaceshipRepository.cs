using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class SpaceshipRepository : Repository, ISpaceshipRepository
    {
        public SpaceshipRepository(SpaceParkContext context, ILogger<Spaceship> logger) : base(context, logger)
        {
        }

        public async Task<Spaceship> GetSpaceshipByPersonNameAsync(string name)
        {
            _logger.LogInformation($"Getting all {name}'s spaceships.");

            var spaceships = _context.People.Where(x => x.Name == name)
                .Include(x => x.Spaceship)
                .Select(x => x.Spaceship);

            return await spaceships.FirstOrDefaultAsync();
        }

        public async Task<Spaceship> ParkShipByName(Spaceship spaceship)
        {
            var person = await _context.People.FirstOrDefaultAsync(x => x.Spaceship.SpaceshipID == spaceship.SpaceshipID);
            Parkinglot currentSpace;

            if (await LoggedIn(person.Name))
            {
                currentSpace = await FindAvailableParkingSpace();

                bool parkingAvailible = int.Parse(spaceship.Length) <= currentSpace.Length;
                if (currentSpace != null)
                {
                    if (parkingAvailible)
                    {
                        _logger.LogInformation($"Parked {spaceship} on parkingspace {currentSpace.ParkinglotID}.");

                        _context.Parkinglot.FirstOrDefault(x => x.ParkinglotID == currentSpace.ParkinglotID)
                       .SpaceshipID = spaceship.SpaceshipID;
                    }

                }
                await _context.SaveChangesAsync();
                spaceship = await GetSpaceshipById(spaceship.SpaceshipID);
            }
            return spaceship;
        }

        public async Task<Spaceship> CheckOutBySpaceshipId(int spaceshipId)
        {
            var person = await _context.People.Include(x => x.Spaceship).FirstOrDefaultAsync(x => x.SpaceshipID == spaceshipId);
            if (!person.HasPaid)
            {
                await PayParking(person);
                // Sets the parkingspaces' shipID back to null.
                _context.Parkinglot.Where(x => x.SpaceshipID == person.SpaceshipID)
                        .FirstOrDefault()
                        .SpaceshipID = null;
                // Nulls a persons current shipID
                await _context.SaveChangesAsync();
                await Delete<Person>(person.PersonID);
                await Delete<Spaceship>(spaceshipId);

            }
            return person.Spaceship;
        }

        public async Task<Spaceship> GetSpaceshipByName(string name)
        {
            _logger.LogInformation($"Getting spaceship named {name}");

            return await _context.Spaceships
                .Where(n => n.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<Spaceship> GetSpaceshipById(int id)
        {
            _logger.LogInformation($"Getting spaceship by id: {id}");

            return await _context.Spaceships
                .Where(n => n.SpaceshipID == id)
                .FirstOrDefaultAsync();
        }
    }
}
