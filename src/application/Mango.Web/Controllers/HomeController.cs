using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mango.Core;
using Mango.Web.Common;
using Newtonsoft.Json;
namespace Mango.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Message()
        {
            return View();
        }
        /// <summary>
        /// 系统异常显示页面
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //记录到日志中
            _logger.LogError(Activity.Current?.Id ?? HttpContext.TraceIdentifier);
            return View();
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var p= HttpContext.Request.Query["p"];
            ViewModels.HomeViewModel viewModel = new ViewModels.HomeViewModel();
            //获取帖子数据
            
            return View(viewModel);
        }
    }
}