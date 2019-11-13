using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Mango.Core;
using Mango.Web.Common;
namespace Mango.Web.Areas.Cms.Controllers
{
    [Area("Cms")]
    public class ChannelController : Controller
    {
        // GET: Channel
        public IActionResult Index()
        {

            var viewModel = LoadMainData(0);

            return View(viewModel);
        }
        [Route("cms/channel/{id}")]
        public IActionResult Index(int id = 0)
        {
            var viewModel = LoadMainData(id);
            return View(viewModel);
        }
        public Models.ChannelViewModel LoadMainData(int id)
        {
            Models.ChannelViewModel viewModel = new Models.ChannelViewModel();
            //获取频道数据
            var apiResult = HttpCore.HttpGet(ApiServerConfig.CMS_GetChannel);

            if (apiResult.Code == 0)
            {
                viewModel.ChannelListData = JsonConvert.DeserializeObject<List<Models.ChannelDataModel>>(apiResult.Data.ToString());
            }

            //获取帖子数据
            int pageIndex = Transform.GetInt(Request.Query["p"], 1);

            apiResult = HttpCore.HttpGet($"{ApiServerConfig.CMS_ContentsList}/{id}/{pageIndex}");

            if (apiResult.Code == 0)
            {
                viewModel.ContentsListData = JsonConvert.DeserializeObject<List<Models.ContentsListDataModel>>(apiResult.Data.ToString());
            }
            return viewModel;
        }
    }
}