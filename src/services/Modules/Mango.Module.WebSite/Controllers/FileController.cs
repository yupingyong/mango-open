using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mango.Module.Core.Entity;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;
using Mango.Framework.Services.UPyun;
using Newtonsoft.Json;
namespace Mango.Module.WebSite.Controllers
{
    [Area("WebSite")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IUPyunService _upyunService;
        public FileController(IUPyunService upyunService)
        {
            _upyunService = upyunService;
        }
        /// <summary>
        /// 获取又拍云存储文件上传
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            string filePath = $"/{DateTime.Now.Year}/{DateTime.Now.Month}/{System.Guid.NewGuid().ToString().Replace("-", "")}/{Path.GetExtension(fileName)}";
            string timeStamp = _upyunService.GetTimeStamp();
            string policy = _upyunService.GetPolicy(filePath, timeStamp);
            var resultData = new
            {
                Expiration = timeStamp,
                Path = filePath,
                Policy= policy,
                Signature= _upyunService.GetSignature(policy)
            };
            return APIReturnMethod.ReturnSuccess(resultData);
        }
    }
}