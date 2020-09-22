using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class PersonRepository : Repository, IPersonRepository
    {
        public PersonRepository(SpaceParkContext context, ILogger<Person> logger) : base(context, logger)
        {
        }

        public async Task<IList<Person>> GetAllPeopleAsync()
        {
            _logger.LogInformation($"Getting all people.");
            return await _context.People.ToListAsync();
        }
        
        public async Task<Person> GetPersonByNameAsync(string name)
        {
            _logger.LogInformation($"Getting all people named {name}");

            return await _context.People
                .Where(n => n.Name == name).Include(p => p.SpaceShip)
                .FirstOrDefaultAsync();
        }
    }
}
