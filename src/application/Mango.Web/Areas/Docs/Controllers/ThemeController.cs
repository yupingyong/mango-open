using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Mango.Core;
using Mango.Web.Common;
namespace Mango.Web.Areas.Docs.Controllers
{
    [Area("Docs")]
    public class ThemeController : Controller
    {
        [Route("{area}/{controller}")]
        [Route("{area}/{controller}/{p}")]
        public IActionResult Index([FromRoute]int p=1)
        {
            Models.ThemeViewModel viewModel = new Models.ThemeViewModel();
            //获取数据
            var apiResult = HttpCore.HttpGet($"{ApiServerConfig.Docs_ThemeApi}/{p}");

            if (apiResult.Code == 0)
            {
                viewModel.ThemeListData = JsonConvert.DeserializeObject<List<Models.ThemeDataModel>>(apiResult.Data.ToString());
            }
            return View(viewModel);
        }
    }
}