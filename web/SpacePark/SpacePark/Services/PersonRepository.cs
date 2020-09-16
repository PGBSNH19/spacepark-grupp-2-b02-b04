using Microsoft.Extensions.Logging;
using SpacePark.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class PersonRepository : Repository, IPersonRepository
    {
        public PersonRepository(SpaceParkContext context, ILogger logger) : base(context, logger)
        {
        }

    }
}
