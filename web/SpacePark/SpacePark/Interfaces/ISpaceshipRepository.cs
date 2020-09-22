using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface ISpaceshipRepository : IRepository
    {
        Task<IList<Spaceship>> GetAllSpaceshipsAsync();
        Task <Spaceship> GetSpaceshipByPersonNameAsync(string name);
        Task<Spaceship> ParkShipByNameAsync(string spaceshipName);
        Task<Person> CheckOutByNameAsync(string shipName);
        Task NullSpaceShipIDInPeopleTable(Person person);
    }
}