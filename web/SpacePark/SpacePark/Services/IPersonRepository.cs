using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface IPersonRepository : IRepository
    {
        Task<IList<Person>> GetAllPeopleAsync();
        Task<IList<Person>> GetPersonByNameAsync(string name);
    }
}