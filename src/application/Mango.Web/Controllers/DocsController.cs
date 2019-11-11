using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Mango.Core;
using Mango.Web.Common;
using Newtonsoft.Json;
namespace Mango.Web.Controllers
{
    /// <summary>
    /// 文档功能模块
    /// </summary>
    public class DocsController : Controller
    {
        /// <summary>
        /// 文档首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewModels.DocsViewModel viewModel = new ViewModels.DocsViewModel();
            //获取文档主题数据
            int pageIndex = Transform.GetInt(Request.Query["p"], 1);
            var requestData = new Dictionary<string, object>();
            requestData.Add("pageIndex", pageIndex);
            var apiResult = HttpCore.HttpGet($"{ApiServerConfig.Docs_GetDocsThemeList}?{HttpParameter.ToUrlParameter(requestData)}");
            if (apiResult.Code == 0)
            {
                viewModel.ListData = JsonConvert.DeserializeObject<List<Models.DocsThemeModel>>(apiResult.Data.ToString());
                viewModel.TotalCount = apiResult.DataCount;
            }
            return View(viewModel);
        }
        /// <summary>
        /// 文档阅读浏览
        /// </summary>
        /// <returns></returns>
        [Route("docs/read/{id}")]
        [Route("docs/read/{id}/{docsid}")]
        public IActionResult Read(int id,int docsid = 0)
        {
            
            ViewModels.DocsReadViewModel viewModel = new ViewModels.DocsReadViewModel();
            viewModel.ThemeId = id;
            viewModel.DocsId = docsid;
            //
            var requestData = new Dictionary<string, object>();
            //获取文档列表数据
            requestData.Add("themeId", id);
            var apiResult = HttpCore.HttpGet($"{ApiServerConfig.Docs_GetDocsList}?{HttpParameter.ToUrlParameter(requestData)}");
            if (apiResult.Code == 0)
            {
                viewModel.ItemsListData = JsonConvert.DeserializeObject<List<Models.DocsListModel>>(apiResult.Data.ToString());
            }
            //获取帖子详情数据
            requestData.Clear();
            if (docsid == 0)
            {
                requestData.Add("themeId", id);
                apiResult = HttpCore.HttpGet($"{ApiServerConfig.Docs_GetDocsThemeById}?{HttpParameter.ToUrlParameter(requestData)}");
                if (apiResult.Code == 0)
                {
                    viewModel.DocsThemeData = JsonConvert.DeserializeObject<Models.DocsThemeModel>(apiResult.Data.ToString());
                }
            }
            else
            {
                requestData.Add("docsId", id);
                apiResult = HttpCore.HttpGet($"{ApiServerConfig.Docs_GetDocsById}?{HttpParameter.ToUrlParameter(requestData)}");
                if (apiResult.Code == 0)
                {
                    viewModel.DocsData = JsonConvert.DeserializeObject<Models.DocsModel>(apiResult.Data.ToString());
                }
            }
            return View(viewModel);
        }
        
    }
}