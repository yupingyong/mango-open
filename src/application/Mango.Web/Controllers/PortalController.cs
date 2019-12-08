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
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            Models.PortalViewModel viewModel = new Models.PortalViewModel();
            var apiResult = HttpCore.HttpGet($"/api/CMS/Contents/customize/new/6");
            if (apiResult.Code == 0)
            {
                viewModel.ContentsListDatas = JsonConvert.DeserializeObject<List<Areas.Cms.Models.ContentsListDataModel>>(apiResult.Data.ToString());
            }
            return View(viewModel);
        }
    }
}