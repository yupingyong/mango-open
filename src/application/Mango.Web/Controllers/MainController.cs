using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Mango.Core;
using Mango.Web.Common;
using Microsoft.AspNetCore.Http;
namespace Mango.Web.Controllers
{
    public class MainController : Controller
    {
        /// <summary>
        /// 又拍云文件上传所需令牌信息
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpGet("{controller}/{action}/{fileName}")]
        public IActionResult UPyun([FromRoute]string fileName)
        {
            var apiResult = HttpCore.HttpGet($"/api/WebSite/File/{fileName}");
            return Json(apiResult);
        }
    }
}