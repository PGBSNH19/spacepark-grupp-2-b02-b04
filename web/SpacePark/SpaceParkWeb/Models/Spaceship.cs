using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaceParkWeb.Models
{
    public class Spaceship
    {
        public int SpaceshipID { get; set; }

        public string Name { get; set; }

        public string Length { get; set; }

        public Spaceship(string name, string length)
        {
            Name = name;
            Length = length;
        }
        public Spaceship(){}
    }
}
