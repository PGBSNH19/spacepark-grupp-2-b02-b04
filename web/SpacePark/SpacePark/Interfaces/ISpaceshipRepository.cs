using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface ISpaceshipRepository : IRepository
    {
        Task<IList<Spaceship>> GetAllSpaceshipsAsync();
        Task<IList<Spaceship>> GetAllSpaceshipsByPersonNameAsync(string name);
        Task<Spaceship> ParkShipByNameAsync(string SpaceshipName);
    }
}