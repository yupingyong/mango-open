using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Mango.Core;
using Mango.Web.Common;
namespace Mango.Web.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class ReleaseController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var apiResult = HttpCore.HttpGet(ApiServerConfig.CMS_GetChannel);
            if (apiResult.Code == 0)
            {
                var channelData = JsonConvert.DeserializeObject<List<Models.ChannelDataModel>>(apiResult.Data.ToString());
                return View(channelData);
            }
            return Json(apiResult);
        }
    }
}