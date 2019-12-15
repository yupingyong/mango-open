using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Mango.Core;
using Mango.Web.Common;
using Microsoft.AspNetCore.Http;
namespace Mango.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class MyController : Controller
    {
        public IActionResult Message()
        {
            return View();
        }
        /// <summary>
        /// 账号信息
        /// </summary>
        /// <returns></returns>
        public IActionResult Info()
        {
            Models.MyAccountViewModel viewModel = new Models.MyAccountViewModel();
            viewModel.AccountData=JsonConvert.DeserializeObject<Models.AccountDataModel>(HttpContext.Session.GetString("AccountLoginData"));
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Information(Models.InformationUpdateRequestModel requestModel)
        {
            requestModel.AccountId = HttpContext.Session.GetInt32("AccountId").GetValueOrDefault(0);

            string requestData = JsonConvert.SerializeObject(requestModel);
            var apiResult = HttpCore.HttpPut($"/api/Account/Information", requestData);
            if (apiResult.Code == 0)
            {
                var accountLoginData= JsonConvert.DeserializeObject<Models.AccountDataModel>(HttpContext.Session.GetString("AccountLoginData"));
                switch (requestModel.InformationType)
                {
                    case 3:
                        accountLoginData.Tags = requestModel.InformationValue;
                        break;
                    case 4:
                        accountLoginData.AddressInfo = requestModel.InformationValue;
                        break;
                    case 5:
                        accountLoginData.Sex = requestModel.InformationValue;
                        break;
                    case 1:
                        accountLoginData.NickName = requestModel.InformationValue;
                        break;
                    case 2:
                        accountLoginData.HeadUrl = requestModel.InformationValue;
                        break;
                }
                HttpContext.Session.SetString("AccountLoginData", JsonConvert.SerializeObject(accountLoginData));
            }
            return Json(apiResult);
        }
        [HttpPost]
        public IActionResult Password(Models.PasswordUpdateRequestModel requestModel)
        {
            requestModel.AccountId = HttpContext.Session.GetInt32("AccountId").GetValueOrDefault(0);

            string requestData = JsonConvert.SerializeObject(requestModel);
            var apiResult = HttpCore.HttpPut($"/api/Account/Password", requestData);
            return Json(apiResult);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Article([FromRoute]int p=1)
        {
            int accountId = HttpContext.Session.GetInt32("AccountId").GetValueOrDefault(0);
            Models.MyArticleViewModel viewModel = new Models.MyArticleViewModel();
            var apiResult = HttpCore.HttpGet($"/api/CMS/Contents/user/list/{accountId}/{p}");

            if (apiResult.Code == 0)
            {
                viewModel.ListData = JsonConvert.DeserializeObject<List<Models.ContentsListDataModel>>(apiResult.Data.ToString());
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Theme([FromRoute]int p = 1)
        {
            int accountId = HttpContext.Session.GetInt32("AccountId").GetValueOrDefault(0);
            Models.MyThemeViewModel viewModel = new Models.MyThemeViewModel();
            var apiResult = HttpCore.HttpGet($"/api/Docs/Theme/user/{accountId}/{p}");

            if (apiResult.Code == 0)
            {
                viewModel.ListData = JsonConvert.DeserializeObject<List<Models.ThemeDataModel>>(apiResult.Data.ToString());
            }
            return View(viewModel);
        }
        [HttpGet("{area}/{controller}/{action}/{themeId}")]
        [HttpGet("{area}/{controller}/{action}/{themeId}/{p}")]
        public IActionResult Document([FromRoute]int themeId,[FromRoute]int p=1)
        {
            int accountId = HttpContext.Session.GetInt32("AccountId").GetValueOrDefault(0);
            Models.MyThemeDocumentViewModel viewModel = new Models.MyThemeDocumentViewModel();
            var apiResult = HttpCore.HttpGet($"/api/Docs/Theme/user/{accountId}/{themeId}/{p}");

            if (apiResult.Code == 0)
            {
                viewModel.ListData = JsonConvert.DeserializeObject<List<Models.DocumentDataModel>>(apiResult.Data.ToString());
            }
            return View(viewModel);
        }
    }
}