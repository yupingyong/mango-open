using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Mango.Framework.Data;
namespace Mango.Module.Core.Controllers
{
    
    [Area("Core")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _uow;
        public AccountController(IUnitOfWork<MangoDbContext> uow)
        {
            _uow = uow;
        }
        // GET: api/Account/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var rep = _uow.GetRepository<Entity.m_Account>();
            var userData = rep.Query().Where(q => q.AccountId == 2).FirstOrDefault();
            return new JsonResult(userData);
        }

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT: api/Account/5
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
