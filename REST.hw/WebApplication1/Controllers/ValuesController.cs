using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using REST.BLL.Models;
using REST.BLL.Services;
using REST.DataAccess;

namespace REST.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/persons")]
    [ApiController]
    public class ValuesController : Controller
    {
        private readonly IPersonService _personPersonService;

        public ValuesController(IPersonService personPersonService)
        {
            _personPersonService = personPersonService;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<List<Person>>> GetAllAsync()
        {
            var result = await _personPersonService.GetAllAsync();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetAsync(int id)
        {
            var result = await _personPersonService.GetByIdAsync(id);
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        [Route("")]
        public async Task<Person> AddPersonAsync([FromBody] UpdatePersonRequest person)
        {
           var result = await _personPersonService.CreatePersonAsync(person);
           return result;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _personPersonService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
