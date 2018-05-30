using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly dataContext _context;

        public ValuesController(dataContext context)
        {
            _context = context;

        }
       
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.values.ToListAsync();
            return Ok(values);
            // throw new Exception("Test exception");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {

            var value1 = await _context.values.FirstOrDefaultAsync(x => x.id == id);
            return Ok(value1);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
