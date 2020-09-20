using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpacePark.Services
{
    public interface IPersonRepository : IRepository
    {
        Task<IList<Person>> GetAllPeopleAsync();
        Task<Person> GetPersonByNameAsync(string name);
    }
}