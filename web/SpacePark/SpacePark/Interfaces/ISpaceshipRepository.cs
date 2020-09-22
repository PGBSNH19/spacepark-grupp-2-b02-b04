﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface ISpaceshipRepository : IRepository
    {
        Task<IList<Spaceship>> GetAllSpaceshipsAsync();
        Task <Spaceship> GetSpaceshipByPersonNameAsync(string name);
        Task<Spaceship> ParkShipByNameAsync(Spaceship spaceship);
        Task<Person> CheckOutByNameAsync(string shipName);
        Task NullSpaceShipIDInPeopleTable(Person person);
        Task<Spaceship> GetSpaceshipByNameAsync(string name);
    }
}