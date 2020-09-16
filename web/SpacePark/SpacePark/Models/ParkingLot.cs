using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpacePark
{
    public class ParkingLot
    {
        public int ParkingLotID { get; set; }
        public int Length { get; set; }
        public int? SpaceShipID { get; set; }
        public SpaceShip? SpaceShip { get; set; }
    }
}
