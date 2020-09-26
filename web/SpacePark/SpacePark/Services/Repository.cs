using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;
using SpacePark.Models;

namespace SpacePark.Services
{
    public class Repository : IRepository
    
    {
        protected readonly SpaceParkContext _context;
        protected readonly ILogger _logger;

        public Repository(SpaceParkContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<T> Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding object of type {entity.GetType()}");
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete<T>(int id)  where T : class
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return entity;
            }
            _logger.LogInformation($"Deleting object of type {entity.GetType()}");
            _context.Set<T>().Remove(entity);
            await Save();
            return entity;
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation("Saving changes");
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<T> Update<T>(T entity)  where T : class
        {
            _logger.LogInformation($"Updating object of type {entity.GetType()}");
            _context.Entry(entity).State = EntityState.Modified;
            await Save();
            return entity;
        }
        public bool LoggedIn(string name)
        {
            if (IsValidPerson(name) && IsPersonInDatabase(name).Result)
            {
                return true;
            }
            return false;
        }

        public async Task<Parkinglot> FindAvailableParkingSpace()
        {
            var parkingSpace = await _context.Parkinglot.FirstOrDefaultAsync(x => x.SpaceshipID == null);

            return parkingSpace;
        }

        public bool IsValidPerson(string name)
        {
            var response = ParkingEngine.GetPersonData(($"people/?search={name}"));

            // Returns false if the person is not in the SWAPI database.
            if (response.Result.Results.Where(p => p.Name == name) != null)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> IsPersonInDatabase(string name)
        {
            var person = await _context.People.Where(x => x.Name == name).FirstOrDefaultAsync();
            return person != null;
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

        public async Task<Person> PayParking(Person person)
        {

            // If the person has not payed, change the value of hasPaid to true in the people table.
            if (!(HasPersonPaid(person).Result))
            {
                _context.People
                    .Where(x => x.Name == person.Name)
                    .FirstOrDefault()
                    .HasPaid = true;
            }

            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> HasPersonPaid(Person p)
        {
            // Finds the person in the people table and checks if the value of hasPaid is true or false,
            // then returns that value.
            var hasPaid = await _context
                .People
                .Where(x => x.Name == p.Name)
                .Select(x => x.HasPaid).FirstOrDefaultAsync();

            return hasPaid;
        }
    }
}