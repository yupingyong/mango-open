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
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Models.AccountRegisterRequestModel requestModel)
        {
            string requestData = JsonConvert.SerializeObject(requestModel);
            var apiResult = HttpCore.HttpPost(ApiServerConfig.Account_Regiser, requestData);
            return Json(apiResult);
        }
    }
}