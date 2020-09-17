using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class SpaceshipRepository : Repository, ISpaceshipRepository
    {
        public SpaceshipRepository(SpaceParkContext context, ILogger<Spaceship> logger) : base(context, logger)
        {
        }

        public async Task <IList<Spaceship>> GetAllSpaceshipsAsync()
        {
            var query = _context.Spaceships;

            _logger.LogInformation($"Getting all spaceships.");

            return await query.ToListAsync();
        }
    }
}
