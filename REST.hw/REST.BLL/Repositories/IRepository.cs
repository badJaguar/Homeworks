using System.Collections.Generic;
using System.Threading.Tasks;
using REST.DataAccess.Models;

namespace REST.DataAccess.Repositories
{
    public interface IRepository
    {
        Task<DbPerson> AddAsync(DbPerson person);
        Task<DbPerson> GetAsync(int id);
        Task<List<DbPerson>> GetAsync();
        Task DeleteAsync(int id);
    }
}