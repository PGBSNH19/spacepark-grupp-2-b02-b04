using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SpaceParkWeb.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public List<string> Starships { get; set; }
        public int? SpaceshipID { get; set; }
        //public Spaceship? CurrentShip { get; set; }
        public bool HasPaid { get; set; } = false;
        
        public Person(int personID, string name, int spaceshipID)
        {
            PersonID = personID;
            Name = name;
            SpaceshipID = spaceshipID;
        }
        public Person() { }
    }
}
