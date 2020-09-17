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

        public async Task <IList<Person>> GetAllPeopleAsync()
        {
            var query = _context.People;

            _logger.LogInformation($"Getting all people.");

            return await query.ToListAsync();
        }

        public async Task<Person> GetPersonByNameAsync(string name)
        {

            _logger.LogInformation($"Getting all people named {name}");

            var query = await _context.People
                .Where(n => n.Name == name)
                .FirstOrDefaultAsync();
            return query;
        }



    }
}
