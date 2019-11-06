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
    public class AddController : ControllerBase
    {
        // POST: api/Posts
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }
    }
}
