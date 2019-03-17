using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using REST.DataAccess.Contexts;
using REST.DataAccess.Models;

namespace REST.DataAccess.Repositories
{
    public class Repository : IRepository
    {
        private readonly PersonContext _context;

        public Repository(PersonContext context)
        {
            _context = context;
        }
        public async Task<DbPerson> AddAsync(DbPerson person)
        {
            // Как это сделать по-человечески?
            //var lastId = _context.Persons.Select(dbPerson => dbPerson.Id).Last();
            //person.Id = lastId + 1;

            var result = await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<DbPerson>> GetAsync()
        {
            var result = await _context.Persons.Select(person => person)
                .ToListAsync();
            return result;
        }

        public async Task<DbPerson> GetAsync(int id)
        {
            var result = await _context.Persons
                .SingleAsync(person => person.Id == id);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var persons = await _context.Persons.Where(person => person.Id == id)
                .ToListAsync();

            _context.Persons.Remove(persons.First());
            await _context.SaveChangesAsync();
        }
    }
}