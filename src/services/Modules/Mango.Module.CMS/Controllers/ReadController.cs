using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Module.CMS.Controllers
{
    [Area("CMS")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {

        // GET: api/Read/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Read
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Read/5
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
