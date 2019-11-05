using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mango.Module.Core.Entity;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;
using Mango.Framework.Services.Tencent.Captcha;
using Mango.Framework.Services.Aliyun.Sms;
namespace Mango.Module.Account.Controllers
{
    [Area("Account")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateCodeController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        private IMemoryCache _memoryCache;
        private ITencentCaptcha _tencentCaptcha;
        private IAliyunSmsSend _aliyunSmsSend;
        public ValidateCodeController(IUnitOfWork<MangoDbContext> unitOfWork
            , IMemoryCache memoryCache
            , ITencentCaptcha tencentCaptcha
            , IAliyunSmsSend aliyunSmsSend)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
            _tencentCaptcha = tencentCaptcha;
            _aliyunSmsSend = aliyunSmsSend;
        }
        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="ticket"></param>
        /// <param name="randstr"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery]string phone, [FromQuery]string ticket, [FromQuery]string randstr)
        {
            string userIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            bool tencentCaptchaResult = _tencentCaptcha.QueryTencentCaptcha(ticket, randstr, userIP);
            if (!tencentCaptchaResult)
            {
                return APIReturnMethod.ReturnFailed("你的验证操作没有通过!");
            }
            Regex regex = new Regex("^1[3456789]\\d{9}$");
            if (!regex.IsMatch(phone))
            {
                return APIReturnMethod.ReturnFailed("请输入正确的手机号验证码!");
            }
            var repository = _unitOfWork.GetRepository<m_Account>();
            if (repository.Query().Where(q=>q.Phone== phone).Count()>0)
            {
                return APIReturnMethod.ReturnFailed("该手机号已经注册过!");
            }
            var smsRepository= _unitOfWork.GetRepository<m_Sms>();
            if (smsRepository.Query().Where(q => q.Phone == phone && q.SendTime.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyMMdd")).Count() >= 5)
            {
                return APIReturnMethod.ReturnFailed("你已经超过短信获取次数限制");
            }
            //短信发送处理
            string PhoneCode = new Random().Next(103113, 985963).ToString();
            //把验证码存储到缓存中
            _memoryCache.Set<string>(phone, PhoneCode);
            //短信验证码发送
            var smsResult = _aliyunSmsSend.SendSmsCode(phone, PhoneCode).Result;
            //插入短信发送记录
            m_Sms model = new m_Sms();
            model.Contents = string.Format("短信验证码为:{0} 服务器返回结果:{1}", PhoneCode, smsResult.response);
            model.IsOk = smsResult.success;
            model.Phone = phone;
            model.SendIP = userIP;
            model.SendTime = DateTime.Now;
            smsRepository.Insert(model);
            _unitOfWork.SaveChanges();
            if (smsResult.success)
            {
                return APIReturnMethod.ReturnSuccess("注册验证码发送成功,请注意查收!");
            }
            return APIReturnMethod.ReturnFailed("注册验证码发送失败,请稍后再试!");
        }
    }
}