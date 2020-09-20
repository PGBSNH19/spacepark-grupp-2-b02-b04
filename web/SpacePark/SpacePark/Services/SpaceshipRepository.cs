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

        public async Task<IList<Spaceship>> GetAllSpaceshipsByPersonName(string name)
        {
            _logger.LogInformation($"Getting all {name}'s spaceships.");
            return await _context.Spaceships.Where(x => x.Name == name).ToListAsync();
        }

        public async Task<Spaceship> ParkShipByNameAsync(string spaceshipName)
        {
            await using var context = new SpaceParkContext();
            var person = context.People.FirstOrDefault(x => x.CurrentShip.Name == spaceshipName);
            Parkinglot currentSpace;

            if (ParkingEngine.LoggedIn(person.Name))
            {
                currentSpace = ParkingEngine.FindAvailableParkingSpace().Result;
                if (currentSpace != null)
                {
                    if (double.Parse(person.CurrentShip.Length) <= currentSpace.Length)
                    {
                        context.Parkinglot.FirstOrDefault(x => x.ParkinglotID == currentSpace.ParkinglotID)
                       .Spaceship = person.CurrentShip;
                    }
                }
                context.SaveChanges();
            }
            return person.CurrentShip;
        }
    }
}
