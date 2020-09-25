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

        public async Task<IList<Spaceship>> GetAllSpaceshipsAsync()
        {
            _logger.LogInformation($"Getting all spaceships.");
            return await _context.Spaceships.ToListAsync();
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

            if (LoggedIn(person.Name))
            {
                currentSpace = await FindAvailableParkingSpace();
                bool parkingAvailible = double.Parse(spaceship.Length) <= currentSpace.Length;
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

        public async Task<bool> CheckOutBySpaceshipId(int spaceshipId)
        {
            var person = await _context.People.SingleOrDefaultAsync(x => x.SpaceshipID == spaceshipId);
            if (person.HasPaid)
            {
                await PayParking(person);
                // Sets the parkingspaces' shipID back to null.
                _context.Parkinglot.Where(x => x.SpaceshipID == person.SpaceshipID)
                        .FirstOrDefault()
                        .SpaceshipID = null;
                // Nulls a persons current shipID
                await NullSpaceShipIDInPeopleTable(person);

                //Removes the curernt person from the person table
                _context.Remove(_context.People
                        .Where(x => x.Name == person.Name)
                        .FirstOrDefault());

                // Borde inte denna och den ovan se exakt lika ut?
                     _context.Remove(_context.Spaceships
                    .Where(x => x.SpaceshipID == person.SpaceshipID)
                    .FirstOrDefault());

            }

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task NullSpaceShipIDInPeopleTable(Person person)
        {
            _context.People.Where(x => x.Name == person.Name)
                .FirstOrDefault().SpaceshipID = null;

            await _context.SaveChangesAsync();
        }

        public async Task<Spaceship> GetSpaceshipByName(string name)
        {
            _logger.LogInformation($"Getting all people named {name}");

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
