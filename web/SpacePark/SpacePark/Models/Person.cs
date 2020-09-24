using Microsoft.AspNetCore.Http.Connections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SpacePark
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<string> Starships { get; set; }
        [NotMapped]
        public List<Spaceship> Spaceships { get; set; }
        public int? SpaceshipID { get; set; }
        public Spaceship Spaceship { get; set; }
        public bool HasPaid { get; set; } = false;

        public static Person CreatePersonFromAPI(string name)
        {
            var response = ParkingEngine.GetPersonData(($"people/?search={name}"));
            var foundPerson = response.Data.Results.FirstOrDefault(p => p.Name == name);

            if (foundPerson != null && foundPerson.Starships != null)
            {
                AddSpaceshipsToPerson(foundPerson);
                //return new Person()
                //{
                //    Name = foundPerson.Name,
                //    Starships = foundPerson.Starships
                //};
                return foundPerson;
            }
            return null;
        }

        // Takes the List of URL's and creates a list of Spaceship objects
        public static void AddSpaceshipsToPerson(Person person)
        {
            person.Spaceships = new List<Spaceship>();
            foreach (var spaceshipUrl in person.Starships)
            {
                person.Spaceships.Add(ParkingEngine.GetSpaceShipData(spaceshipUrl));
            }
        }
    }
}
