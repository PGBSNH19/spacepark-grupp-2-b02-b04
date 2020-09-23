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

        public async Task<Spaceship> ParkShipByNameAsync(Spaceship spaceship)
        {
            //await using var context = new SpaceParkContext();
            var person = await _context.People.FirstOrDefaultAsync(x => x.Spaceship.SpaceshipID == spaceship.SpaceshipID);
            Parkinglot currentSpace;

            if (LoggedIn(person.Name))
            {
                currentSpace = await FindAvailableParkingSpace();
                if (currentSpace != null)
                {
                    //if (double.Parse(spaceship.Length) <= currentSpace.Length)
                    //{
                    //    _logger.LogInformation($"Parked {spaceship} on parkingspace {currentSpace.ParkinglotID}.");

                    //    _context.Parkinglot.FirstOrDefault(x => x.ParkinglotID == currentSpace.ParkinglotID)
                    //   .SpaceshipID = spaceship.SpaceshipID;
                    //}

                    _logger.LogInformation($"Parked {spaceship} on parkingspace {currentSpace.ParkinglotID}.");

                    _context.Parkinglot.FirstOrDefault(x => x.ParkinglotID == currentSpace.ParkinglotID)
                   .SpaceshipID = spaceship.SpaceshipID;
                }
                _context.SaveChanges();
            }
            return spaceship;
        }

        public async Task<Person> CheckOutByNameAsync(string shipName)
        {
            await using var context = new SpaceParkContext();
            var person = context.People.SingleOrDefaultAsync(x => x.Spaceship.Name == shipName).Result;
            await PayParking(person);

            if (person.HasPaid)
            {
                // Sets the parkingspaces' shipID back to null.
                context.Parkinglot.Where(x => x.SpaceshipID == person.SpaceshipID)
                    .FirstOrDefault()
                    .SpaceshipID = null;

                // Nulls a persons current shipID
                await NullSpaceShipIDInPeopleTable(person);

                //Removes the curernt person from the person table
                context.Remove(context.People
                    .Where(x => x.Name == person.Name)
                    .FirstOrDefault());

                // Borde inte denna och den ovan se exakt lika ut?
                var spaceship = context.Spaceships
                    .Where(x => x.SpaceshipID == person.SpaceshipID)
                    .FirstOrDefault();

                context.Remove(spaceship);

                context.SaveChanges();
            }
            return person;
        }

        public async Task NullSpaceShipIDInPeopleTable(Person person)
        {
            await using var context = new SpaceParkContext();

            context.People.Where(x => x.Name == person.Name)
                .FirstOrDefault().SpaceshipID = null;

            context.SaveChanges();
        }

        public async Task<Spaceship> GetSpaceshipByNameAsync(string name)
        {
            _logger.LogInformation($"Getting all people named {name}");

            return await _context.Spaceships
                .Where(n => n.Name == name)
                .FirstOrDefaultAsync();
        }
    }
}
