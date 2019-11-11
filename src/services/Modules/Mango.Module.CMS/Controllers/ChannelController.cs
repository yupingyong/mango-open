using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;

namespace Mango.Module.CMS.Controllers
{
    [Area("CMS")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        public ChannelController(IUnitOfWork<MangoDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 获取频道数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var repository = _unitOfWork.GetRepository<Entity.m_CmsChannel>();
            var resultData = repository.Query().OrderBy(q => q.SortCount).ToList();
            return APIReturnMethod.ReturnSuccess(resultData);
        }
    }
}