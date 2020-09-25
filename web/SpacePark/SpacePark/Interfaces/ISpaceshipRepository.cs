using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface ISpaceshipRepository : IRepository
    {
        Task <Spaceship> GetSpaceshipByPersonNameAsync(string name);
        Task<bool> CheckOutBySpaceshipId(int id);
        Task NullSpaceShipIDInPeopleTable(Person person);
        Task<Spaceship> GetSpaceshipByName(string name);
        Task<Spaceship> GetSpaceshipById(int id);
        Task<Spaceship> ParkShipByName(Spaceship spaceship);
    }
}