using SpacePark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Db_Context
{
    public class DbInitizalizer
    {
        public void Initialize(SpaceParkContext context)
        {
            context.Database.EnsureCreated();

            if (context.Spaceships.Any())
            {
                return;
            }

            // Makes sure there are 10 rows to the parkinglot table.
            for (int i = context.Parkinglot.Count(); i < 10; i++)
            {
                var parkingSpace = new Parkinglot
                {
                    Length = 50,
                    Spaceship = null
                };
                context.Parkinglot.Add(parkingSpace);
            }
            context.SaveChanges();
        }

    }
}
