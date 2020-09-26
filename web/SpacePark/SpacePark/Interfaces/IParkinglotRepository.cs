

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface IParkinglotRepository : IRepository
    {
        Task<IList<Parkinglot>> GetAllParkinglots();
        Task<Parkinglot> GetParkinglotById(int id);
        Task<Parkinglot> FindAvailableParkingSpace();
    }
}