using Microsoft.Extensions.Logging;
using SpacePark.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class ParkinglotRepository : Repository, IParkinglotRepository
    {
        public ParkinglotRepository(SpaceParkContext context, ILogger logger) : base(context, logger)
        {
        }

    }
}
