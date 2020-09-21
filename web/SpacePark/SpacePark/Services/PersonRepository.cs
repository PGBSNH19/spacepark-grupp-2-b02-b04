using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SpacePark.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public class PersonRepository : Repository, IPersonRepository
    {
        public PersonRepository(SpaceParkContext context, ILogger<Person> logger) : base(context, logger)
        {
        }

        public async Task<IList<Person>> GetAllPeopleAsync()
        {
            _logger.LogInformation($"Getting all people.");
            return await _context.People.ToListAsync();
        }
        public bool LoggedIn(string name)
        {
            if (IsValidPerson(name) && IsPersonInDatabase(name).Result)
            {
                return true;
            }
            return false;
        }

        public async Task<Person> GetPersonByNameAsync(string name)
        {
            _logger.LogInformation($"Getting all people named {name}");

            return await _context.People
                .Where(n => n.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsPersonInDatabase(string name)
        {
            await using var context = new SpaceParkContext();
            var person = context.People.Where(x => x.Name == name).FirstOrDefault();

            if (person != null && person.Name == name)
            {
                return true;
            }
            return false;
        }

        public Person CheckIn(string name)
        {
            var person = new Person();

            if (IsValidPerson(name) && !IsPersonInDatabase(name).Result)
            {
                person = Person.CreatePersonFromAPI(name);
            }
            return person;
        }

        public async Task<bool> HasPersonPaid(Person p)
        {
            await using var context = new SpaceParkContext();

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

        public async Task<Person> PayParking(Person person)
        {
            await using var context = new SpaceParkContext();

            // If the person has not payed, change the value of hasPaid to true in the people table.
            if (!(HasPersonPaid(person).Result))
            {
                context.People
                    .Where(x => x.Name == person.Name)
                    .FirstOrDefault()
                    .HasPaid = true;
            }

            context.SaveChanges();
            return person;
        }

        public bool IsValidPerson(string name)
        {
            var response = ParkingEngine.GetPersonData(($"people/?search={name}"));

            // Returns false if the person is not in the SWAPI database.
            if (response.Data.Results.Where(p => p.Name == name) != null)
            {
                return true;
            }
            return false;
        }
    }
}
