using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Mango.Core;
using Mango.Web.Common;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860
namespace Mango.Web.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Index()
        {
            
            ViewModels.PostsViewModel viewModel = LoadMainData(0);
            
            return View(viewModel);
        }
        [Route("posts/channel/{id}")]
        public IActionResult Index(int id = 0)
        {
            ViewModels.PostsViewModel viewModel = LoadMainData(id);
            return View(viewModel);
        }
        public ViewModels.PostsViewModel LoadMainData(int id)
        {
            ViewModels.PostsViewModel viewModel = new ViewModels.PostsViewModel();
            //获取频道数据
            var apiResult = HttpCore.HttpGet(ApiServerConfig.CMS_GetChannel);
            if (apiResult.Code == 0)
            {
                viewModel.PostsChannelData = JsonConvert.DeserializeObject<List<Models.PostsChannelModel>>(apiResult.Data.ToString());
            }

            //获取帖子数据
            int pageIndex = Transform.GetInt(Request.Query["p"], 1);
            var requestData = new Dictionary<string, object>();
            requestData.Add("channelId", id);
            requestData.Add("pageIndex", pageIndex);
            apiResult = HttpCore.HttpGet($"{ApiServerConfig.Posts_GetPostsList}?{HttpParameter.ToUrlParameter(requestData)}");
            if (apiResult.Code == 0)
            {
                viewModel.ListData = JsonConvert.DeserializeObject<List<Models.PostsModel>>(apiResult.Data.ToString());
                viewModel.TotalCount = apiResult.DataCount;
            }
            return viewModel;
        }
        /// <summary>
        /// 帖子内容阅读
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Read(int id)
        {
            ViewModels.PostsReadViewModel viewModel = new ViewModels.PostsReadViewModel();
            //获取帖子详情数据
            var requestData = new Dictionary<string, object>();
            requestData.Add("postsId", id);
            var apiResult = HttpCore.HttpGet($"{ApiServerConfig.Posts_GetPostsById}?{HttpParameter.ToUrlParameter(requestData)}");
            if (apiResult.Code == 0)
            {
                viewModel.PostsData = JsonConvert.DeserializeObject<Models.PostsModel>(apiResult.Data.ToString());
            }
            //获取帖子回复数据
            int pageIndex = Transform.GetInt(Request.Query["p"], 1);
            requestData = new Dictionary<string, object>();
            requestData.Add("postsId", id);
            requestData.Add("pageIndex", pageIndex);

            apiResult = HttpCore.HttpGet($"{ApiServerConfig.Posts_GetPostsAnswerList}?{HttpParameter.ToUrlParameter(requestData)}");
            if (apiResult.Code == 0)
            {
                viewModel.AnswerListData = JsonConvert.DeserializeObject<List<Models.PostsAnswerModel>>(apiResult.Data.ToString());
                viewModel.TotalCount = apiResult.DataCount;
            }
            //获取热门帖子数据
            apiResult = HttpCore.HttpGet(ApiServerConfig.Posts_GetPostsList);
            if (apiResult.Code == 0)
            {
                viewModel.HotListData = JsonConvert.DeserializeObject<List<Models.PostsModel>>(apiResult.Data.ToString());
            }
            return View(viewModel);
        }
    }
}
