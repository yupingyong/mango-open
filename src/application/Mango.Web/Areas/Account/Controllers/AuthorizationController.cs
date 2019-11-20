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

namespace Mango.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class AuthorizationController : Controller
    {
        /// <summary>
        /// 邮箱验证码发送
        /// </summary>
        /// <param name="email"></param>
        /// <param name="ticket"></param>
        /// <param name="randstr"></param>
        /// <returns></returns>
        public string SendEmailValidateCode(string email, string ticket, string randstr)
        {
            string userIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            //获取频道数据
            var apiResult = HttpCore.HttpGet($"{ApiServerConfig.Account_AuthorizationValidateCode}/{email}/{ticket}/{randstr}/{userIP}");
            return apiResult.Message;
        }
    }
}