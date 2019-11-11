using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Core;
using Mango.Web.Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkId=397860

namespace Mango.Web.Controllers
{
    public class NavigationController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            
            ViewModels.NavigationViewModel viewModel = new ViewModels.NavigationViewModel();
            var requestData = new Dictionary<string, object>();
            //获取网址导航分类数据
            var apiResult = HttpCore.HttpGet(ApiServerConfig.Navigation_GetNavigationClassify);
            if (apiResult.Code == 0)
            {
                viewModel.ClassifyListData = JsonConvert.DeserializeObject<List<Models.NavigationClassifyModel>>(apiResult.Data.ToString());
            }
            //获取网址导航数据
            apiResult = HttpCore.HttpGet(ApiServerConfig.Navigation_GetNavigationList);
            if (apiResult.Code == 0)
            {
                viewModel.NavigationsData = JsonConvert.DeserializeObject<List<Models.NavigationModel>>(apiResult.Data.ToString());
            }
            return View(viewModel);
        }
        public bool UpdateClickCount(int navigationId)
        {
            return true;
            //NavigationRepository repository = new NavigationRepository();
            //return repository.UpdateClickCount(navigationId);
        }
    }
}
