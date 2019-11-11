using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core
{
    public class ApiResult
    {
        /// <summary>
        /// 结果码(0 表示接口正常)
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 数据记录条数
        /// </summary>
        public int DataCount { get; set; } = 0;
    }
}
