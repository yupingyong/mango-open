using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mango.Module.Core.Entity;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;
namespace Mango.Module.Account.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateCodeController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        private IMemoryCache _memoryCache;
        public ValidateCodeController(IUnitOfWork<MangoDbContext> unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public IActionResult Get([FromQuery]string phone, [FromQuery]string ticket, [FromQuery]string randstr)
        {
            
        }
    }
}