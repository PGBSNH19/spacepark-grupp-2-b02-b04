using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkWeb.Models
{
    public class Starships
    {
        public int SpaceshipID { get; set; }

        public string Name { get; set; }

        public string Length { get; set; }

        public Starships(string name, string length)
        {
            Name = name;
            Length = length;
        }
    }
}
