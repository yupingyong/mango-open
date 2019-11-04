using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.Core.Common.Aliyun
{
    public class SmsConfig
    {
        /// <summary>
        /// 短信签名
        /// </summary>
        public string SmsSignature { get; set; }
        /// <summary>
        /// 短信模板
        /// </summary>
        public string SmsTempletKey { get; set; }
    }
}
