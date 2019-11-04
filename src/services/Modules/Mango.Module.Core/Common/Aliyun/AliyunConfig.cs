using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.Core.Common.Aliyun
{
    public class AliyunConfig
    {
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
        /// <summary>
        /// 短信配置项
        /// </summary>
        public Sms.SmsConfig Sms { get; set; }
    }
}
