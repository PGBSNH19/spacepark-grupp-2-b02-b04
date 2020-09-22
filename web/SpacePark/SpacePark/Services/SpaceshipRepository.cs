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

        public async Task<IList<Spaceship>> GetAllSpaceshipsByPersonNameAsync(string name)
        {
            _logger.LogInformation($"Getting all {name}'s spaceships.");

            List<Spaceship> spaceships = await _context.People.Where(x => x.Name == name).Include(x => x.Spaceships).Select(x => x.Spaceships).FirstOrDefaultAsync();

            return spaceships;
        }

        public async Task<Spaceship> ParkShipByNameAsync(string spaceshipName)
        {
            await using var context = new SpaceParkContext();
            var person = context.People.FirstOrDefault(x => x.SpaceShip.Name == spaceshipName);
            Parkinglot currentSpace;

            if (LoggedIn(person.Name))
            {
                currentSpace = FindAvailableParkingSpace().Result;
                if (currentSpace != null)
                {
                    if (double.Parse(person.SpaceShip.Length) <= currentSpace.Length)
                    {
                        _logger.LogInformation($"Parked {person.SpaceShip} on parkingspace {currentSpace.ParkinglotID}.");

                        context.Parkinglot.FirstOrDefault(x => x.ParkinglotID == currentSpace.ParkinglotID)
                       .Spaceship = person.SpaceShip;
                    }
                }
                context.SaveChanges();
            }
            return person.SpaceShip;
        }

        public async Task<Person> CheckOutByNameAsync(string shipName)
        {
            await using var context = new SpaceParkContext();
            var person = context.People.SingleOrDefaultAsync(x => x.SpaceShip.Name == shipName).Result;
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
    }
}
