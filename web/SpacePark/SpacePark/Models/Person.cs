using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SpacePark
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public List<string> Starships { get; set; }
        public int? SpaceShipID { get; set; }
        public SpaceShip? CurrentShip { get; set; }
        public bool HasPaid { get; set; } = false;

        public static Person CreatePersonFromAPI(string name)
        {
            var response = ParkingEngine.GetPersonData(($"people/?search={name}"));
            var foundPerson = response.Data.Results.FirstOrDefault(p => p.Name == name);

            if (foundPerson != null)
            {
                return new Person()
                {
                    Name = foundPerson.Name,
                    Starships = foundPerson.Starships,
                };
            }
            return null;
        }
    }
}
