using Microsoft.AspNetCore.Http.Connections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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

        public async static Task<Person> CreatePersonFromAPI(string name)
        {
            var response = await ParkingEngine.GetPersonData(($"people/?search={name}"));
            var foundPerson = response.Results.FirstOrDefault(p => p.Name == name);

            if (foundPerson != null && foundPerson.Starships != null)
            {
                foundPerson.Spaceships = await AddSpaceshipsToPerson(foundPerson);
                return foundPerson;
            }
            return null;
        }

        // Takes the List of URL's and creates a list of Spaceship objects
        public async static Task<List<Spaceship>> AddSpaceshipsToPerson(Person person)
        {
            List<Spaceship> spaceships = new List<Spaceship>();
            person.Spaceships = new List<Spaceship>();
            foreach (var spaceshipUrl in person.Starships)
            {
                spaceships.Add((await ParkingEngine.GetSpaceShipData(spaceshipUrl)));
            }
            return spaceships;
        }
    }
}
