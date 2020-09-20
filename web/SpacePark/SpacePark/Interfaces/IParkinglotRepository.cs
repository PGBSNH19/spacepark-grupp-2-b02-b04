

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface IParkinglotRepository : IRepository
    {
        Task<IList<Parkinglot>> GetAllParkinglotsAsync();
        Task<Parkinglot> GetParkinglotByIdAsync(int id);
    }
}