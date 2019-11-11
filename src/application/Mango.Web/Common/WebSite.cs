using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mango.Core;
using Newtonsoft.Json;
namespace Mango.Web.Common
{
    public class WebSite
    {
        /// <summary>
        /// 获取网站设置基础数据
        /// </summary>
        /// <returns></returns>
        public PageModels.WebSitePageModel GetWebSiteData()
        {
            PageModels.WebSitePageModel pageModel = new PageModels.WebSitePageModel();
            var apiResult = HttpCore.HttpGet($"{ApiServerConfig.ApiServerUrl}{ApiServerConfig.MainData_GetWebSiteNavigation}");
            if (apiResult.Code == 0)
            {
                pageModel.WebSiteNavigationData = JsonConvert.DeserializeObject<List<Models.WebSiteNavigationModel>>(apiResult.Data.ToString());
            }
            apiResult= HttpCore.HttpGet($"{ApiServerConfig.ApiServerUrl}{ApiServerConfig.MainData_GetWebSiteConfig}");
            if (apiResult.Code == 0)
            {
                pageModel.WebSiteConfigData = JsonConvert.DeserializeObject<Models.WebSiteConfigModel>(apiResult.Data.ToString());
            }
            return pageModel;
        }
    }
}
