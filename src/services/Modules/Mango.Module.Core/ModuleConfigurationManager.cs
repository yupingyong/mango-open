using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.Core
{
    /// <summary>
    /// 模块共用配置项管理类
    /// </summary>
    public static class ModuleConfigurationManager
    {
        /// <summary>
        /// 阿里云配置项
        /// </summary>
        public static Common.Aliyun.AliyunConfig Aliyun { get; set; }
        /// <summary>
        /// 腾讯配置项信息
        /// </summary>
        public static Common.Tencent.TencentConfig Tencent { get; set; }
    }
}
