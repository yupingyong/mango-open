using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.Core.Common.Tencent
{
    public class TencentConfig
    {
        /// <summary>
        /// 腾讯云验证码配置项
        /// </summary>
        public Captcha.CaptchaConfig Captcha { get; set; }
    }
}
