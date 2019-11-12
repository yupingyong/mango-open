using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Areas.Docs.Controllers
{
    public class ReleaseController : Controller
    {
        /// <summary>
        /// 发布文档主题
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Theme()
        {
            return View();
        }
        /// <summary>
        /// 发布文档
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Document()
        {
            return View();
        }
    }
}