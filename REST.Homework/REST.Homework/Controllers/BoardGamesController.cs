using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace REST.Homework.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/board-games")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetAll()
        {
            return new Task<IActionResult>(() => Accepted());
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetById(int id)
        {
            return new Task<IActionResult>(() => Accepted());
        }
    }
}