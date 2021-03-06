﻿using RestSharp;
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
        public List<Spaceship> Spaceships { get; set; }
        public List<string> Starships { get; set; }
        public int? SpaceshipID { get; set; }
        public Spaceship? Spaceship { get; set; }
        public bool HasPaid { get; set; } = false;

        public Person(){}
        public Person(int personID, string name, int spaceshipID, List<Spaceship> spaceships, Spaceship spaceship)
        {
            PersonID = personID;
            Name = name;
            SpaceshipID = spaceshipID;
            Spaceship = spaceship;
            Spaceships = spaceships;
        }
    }
}
