using System.Collections.Generic;
using System.Threading.Tasks;
using REST.BLL.Models;
using REST.DataAccess;

namespace REST.BLL.Services
{
    public interface IPersonService
    {
        Task<Person> CreatePersonAsync(UpdatePersonRequest person);
        Task<Person> GetByIdAsync(int id);
        Task<List<Person>> GetAllAsync();
        Task DeleteByIdAsync(int id);
    }
}