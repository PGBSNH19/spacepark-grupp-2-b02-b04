using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpacePark.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace SpacePark
{
    public class ParkingEngine
    {
        public static IRestResponse<PersonResult> GetPersonData(string input)
        {
            var client = new RestClient("https://swapi.dev/api/");
            var request = new RestRequest(input, DataFormat.Json);
            var apiResponse = client.Get<PersonResult>(request);

            return apiResponse;
        }

        public static Spaceship GetSpaceShipData(string input)
        {
            var client = new RestClient(input);
            var request = new RestRequest("", DataFormat.Json);
            var apiResponse = client.ExecuteAsync<Spaceship>(request);
            apiResponse.Wait();

            return apiResponse.Result.Data;
        }

        public static bool IsValidPerson(string name)
        {
            var response = GetPersonData(($"people/?search={name}"));

            // Returns false if the person is not in the SWAPI database.
            if (response != null)
            {
                foreach (var p in response.Data.Results)
                {
                    if (p.Name == name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async static Task<bool> IsPersonInDatabase(string name)
        {
            // Returns true if a person with tha maching name is stored in the people table.
            using (var context = new SpaceParkContext())
            {
                var person = context.People.Where(x => x.Name == name).FirstOrDefault();

                if (person != null && person.Name == name)
                {
                    return true;
                }

                return false;
            }
        }

        public static async Task ParkShip(Person p)
        {
            Parkinglot currentSpace;

            using (var context = new SpaceParkContext())
            {
                try
                {
                    currentSpace = FindAvailableParkingSpace().Result;

                    // If the ship is smaller than the parkingspace park in the space => park it.
                    if (double.Parse(p.CurrentShip.Length) <= currentSpace.Length)
                    {
                        context.Parkinglot.Where(x => x.ParkinglotID == currentSpace.ParkinglotID)
                       .FirstOrDefault()
                       .Spaceship = p.CurrentShip;
                    }
                    else
                    {
                        Console.WriteLine("Sorry your ship is to big!");
                        Thread.Sleep(2500);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("No avaialable parking spaces!");
                    Thread.Sleep(2500);
                }

                context.Spaceships.Add(p.CurrentShip);
                context.People.Add(p);
                // Adds the person and the ship to the appropriate table then saves the changes.
                context.SaveChanges();

                Console.WriteLine("Your ship has been parked.");
                Thread.Sleep(2500);
            }
        }

        public static async Task<Parkinglot> FindAvailableParkingSpace()
        {
            using (var context = new SpaceParkContext())
            {
                // Finds the first available (where SpaceShipID == null) parkingspot, and then returns that spot.
                var parkingSpace = context.Parkinglot.FirstOrDefault(x => x.SpaceshipID == null);
                if (parkingSpace == null)
                {
                    // Should throw an exception here but does not work with catch.
                    Console.WriteLine();
                    Console.WriteLine("There aren't any parking spaces available.");
                    Thread.Sleep(2500);
                    return null;
                }
                return parkingSpace;
            }
        }

        public static void CheckIn(string name, string spaceship)
        {

            // If the person is in starwars and isn't in the database.
            if (ParkingEngine.IsValidPerson(name) && !IsPersonInDatabase(name).Result)
            {
                // Creates the person obejct.
                var person = Person.CreatePersonFromAPI(name, spaceship);

                //Console.WriteLine("Enter the number of the ship do you want to park:");
                //int count = 0;

                //// Prints all starships associated with person.
                //foreach (var item in person.Starships)
                //{
                //    count++;
                //    var s = ParkingEngine.GetSpaceShipData(item);

                //    Console.WriteLine($"{count}.{s.Name}");
                //}

                //// Saves the users selection.
                //var shipNumber = int.Parse(Console.ReadLine());

                //// Actually creates the ship object.
                //var spaceShip = Spaceship.CreateStarshipFromAPI(person.Starships[shipNumber - 1]);
                //person.CurrentShip = spaceShip;

                // Adds the person and ship to the database.
                ParkingEngine.ParkShip(person);

            }

            else if (!ParkingEngine.IsValidPerson(name))
            {
                Console.WriteLine();
                Console.WriteLine("Sorry you have to have been in Star Wars to park here.");
                Thread.Sleep(2500);
            }

            else
            {
                Console.WriteLine();
                Console.WriteLine("You have already parked here.");
                Thread.Sleep(2500);
            }
        }

        public static async Task CheckOut(Person p)
        {
            if (p.HasPaid)
            {
                using (var context = new SpaceParkContext())
                {
                    // Sets the parkingspaces' shipID back to null.
                    context.Parkinglot.Where(x => x.SpaceshipID == p.SpaceshipID)
                        .FirstOrDefault()
                        .SpaceshipID = null;

                    // Nulls a persons current shipID
                    await NullSpaceShipIDInPeopleTable(p, context);

                    //Removes the curernt person from the person table
                    context.Remove(context.People
                        .Where(x => x.Name == p.Name)
                        .FirstOrDefault());

                    // Borde inte denna och den ovan se exakt lika ut?
                    var temp = context.Spaceships.Where(x => x.SpaceshipID == p.SpaceshipID)
                        .FirstOrDefault();

                    context.Remove(temp);

                    context.SaveChanges();
                }
                Console.WriteLine();
                Console.WriteLine("You have been checked out!");
                Thread.Sleep(2500);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Sorry you have to pay first!");
                Thread.Sleep(2500);
            }
        }

        private static async Task NullSpaceShipIDInPeopleTable(Person p, SpaceParkContext context)
        {
            // Find the person in the people table and sets the spaceshipID to null.
            context.People.Where(x => x.Name == p.Name)
                .FirstOrDefault().SpaceshipID = null;

            context.SaveChanges();
        }

        public static async Task ClearParkedShip(Spaceship spaceShip)
        {
            using (var context = new SpaceParkContext())
            {
                // Finds the ship in the person table and set it to null.
                context.Parkinglot.Where(x => x.SpaceshipID == spaceShip.SpaceshipID)
                    .FirstOrDefault()
                    .Spaceship = null;

                context.SaveChanges();
            }
        }

        public static async Task<bool> HasPersonPaid(Person p)
        {
            using (var context = new SpaceParkContext())
            {
                // Finds the person in the people table and checks if the value of hasPaid is true or false,
                // then returns that value.
                var hasPaid = context
                    .People
                    .Where(x => x.Name == p.Name)
                    .FirstOrDefault().HasPaid;

                if (hasPaid)
                {
                    return true;
                }
                return false;
            }

        }

        public static async Task PayParking(Person p)
        {
            using (var context = new SpaceParkContext())
            {
                Console.WriteLine();

                // If the person has not payed, change the value of hasPaid to true in the people table.
                if (!(HasPersonPaid(p).Result))
                {
                    context.People
                        .Where(x => x.Name == p.Name)
                        .FirstOrDefault()
                        .HasPaid = true;

                    Console.WriteLine("Parking has been paid & you are now ready to check out!");
                    Thread.Sleep(2500);
                }
                else
                {
                    Console.WriteLine("You have already paid!");
                    Thread.Sleep(2500);
                }

                context.SaveChanges();
            }
        }

        public static async Task<Person> GetPersonFromDatabase(string name)
        {
            using (var context = new SpaceParkContext())
            {
                // return the person object from the people table with a matching name.
                return context.People
                    .Where(x => x.Name == name)
                    .FirstOrDefault();
            }
        }




    }
}
