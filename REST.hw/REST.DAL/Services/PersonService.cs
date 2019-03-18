using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
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
        private IMemoryCache _cache;

        public PersonService(IMapper mapper, IRepository repo, IMemoryCache cache)
        {
            _mapper = mapper;
            _repo = repo;
            _cache = cache;
        }

        public async Task<Person> CreatePersonAsync(UpdatePersonRequest person)
        {
            var dbPerson = _mapper.Map<UpdatePersonRequest, DbPerson>(person);
            var source = await _repo.AddAsync(dbPerson);
            var result = _mapper.Map<DbPerson, Person>(source);

            // Не уверен, верно ли вносить в кэш данные на этом уровне или же лучше на уровне репозитория?
            if (result != null)
            {
                _cache.Set(result.Id, result, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15)
                });
            }
            return result;
        }

        public async Task<List<Person>> GetAllAsync()
        {
            var list = await _repo.GetAsync();
            foreach (var person in list)
                if (!_cache.TryGetValue(person.Id, out DbPerson value))
                {
                    value = await _repo.GetAsync(person.Id);

                    if (value != null)
                        _cache.Set(value.Id, value,
                            new MemoryCacheEntryOptions()
                                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));
                }

            return list.Select(_mapper.Map<Person>).ToList();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            if (!_cache.TryGetValue(id, out DbPerson person))
            {
                person = await _repo.GetAsync(id);
                if (person != null)
                {
                    _cache.Set(person.Id, person,
                        new MemoryCacheEntryOptions()
                            .SetAbsoluteExpiration(TimeSpan.FromMinutes(15)));
                }
            }

            return _mapper.Map<Person>(person);
        }

        public async Task DeleteByIdAsync(int id) => await _repo.DeleteAsync(id);
    }
}
