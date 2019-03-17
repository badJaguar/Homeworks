using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using REST.BLL.Models;
using REST.DataAccess;
using REST.DataAccess.Models;
using REST.DataAccess.Repositories;

namespace REST.BLL.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repo;

        public PersonService(IMapper mapper, IRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        public async Task<Person> CreatePersonAsync(UpdatePersonRequest person)
        {
            var dbPerson = _mapper.Map<UpdatePersonRequest, DbPerson>(person);
            var result = await _repo.AddAsync(dbPerson);
            return _mapper.Map<DbPerson, Person>(result);
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var list = await _repo.GetAsync();
            return list.Select(model => _mapper.Map<Person>(model)).ToList();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            var result = await _repo.GetAsync(id);
            return _mapper.Map<Person>(result);
        }

        public async Task DeleteByIdAsync(int id) => await _repo.DeleteAsync(id);
    }
}
