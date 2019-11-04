using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.Core.Common.Aliyun
{
    public static class AliyunConfig
    {
        public static string AccessKeyId { get; set; }
        public static string AccessKeySecret { get; set; }
        /// <summary>
        /// 短信配置项
        /// </summary>
        public static Sms.SmsConfig Sms { get; set; }
    }
}
