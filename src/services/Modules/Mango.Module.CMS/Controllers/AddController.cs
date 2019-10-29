using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Module.Posts.Controllers
{
    [Area("Posts")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AddController : ControllerBase
    {

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
