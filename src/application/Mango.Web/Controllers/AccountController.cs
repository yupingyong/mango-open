using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.DrawingCore;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using Mango.Core;
using Mango.Web.Common;
namespace Mango.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(ViewModels.AccountLoginRequestModel requestModel)
        {
            string postData = JsonConvert.SerializeObject(requestModel);
            var apiResult = HttpCore.HttpPost(ApiServerConfig.Account_Login, postData);
            if (apiResult.Code == 0)
            {
                Models.UserInfoModel model = JsonConvert.DeserializeObject<Models.UserInfoModel>(apiResult.Data.ToString());
                //将登陆的用户Id存储到会话中
                HttpContext.Session.SetString("UserId", model.UserId.ToString());
                HttpContext.Session.SetString("GroupId", model.GroupId.ToString());
                HttpContext.Session.SetString("AccountName", model.AccountName);
                HttpContext.Session.SetString("NickName", model.NickName);
                HttpContext.Session.SetString("HeadUrl", model.HeadUrl);
                HttpContext.Session.SetString("UserLogin", apiResult.Data.ToString());
            }
            return Json(apiResult);
        }
    }
}