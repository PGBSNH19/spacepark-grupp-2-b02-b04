using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark
{
    public class Parkinglot
    {
        public int ParkinglotID { get; set; }
        public int Length { get; set; }
        public int? SpaceshipID { get; set; }
        public Spaceship? Spaceship { get; set; }
    }
}
