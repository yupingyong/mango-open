using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Core
{
    /// <summary>
    /// HTTP请求参数处理类
    /// </summary>
    public class HttpParameter
    {
        /// <summary>
        /// 将集合转换成URL参数形式
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string ToUrlParameter(Dictionary<string, object> parameters)
        {
            if (parameters != null)
            {
                StringBuilder urlParameterResult = new StringBuilder();
                foreach (var p in parameters)
                {
                    if (urlParameterResult.Length>0)
                    {
                        urlParameterResult.Append("&");
                    }
                    urlParameterResult.AppendFormat("{0}={1}", p.Key, p.Value);
                }
                return urlParameterResult.ToString();
            }
            return string.Empty;
        }
    }
}
