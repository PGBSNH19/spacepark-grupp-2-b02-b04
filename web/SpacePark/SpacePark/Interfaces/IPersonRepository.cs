﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface IPersonRepository : IRepository
    {
        Task<IList<Person>> GetAllPeople();
        Task<Person> GetPersonByName(string name);
        Task<bool> LoggedIn(string name);
        Task<bool> IsPersonInDatabase(string name);
        Task<Person> CheckIn(string name);
        Task<bool> HasPersonPaid(Person p);
        Task<Person> PayParking(Person person);
        Task<bool> IsValidPerson(string name);
    }
}