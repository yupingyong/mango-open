using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 自定义分页
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="rowCount">当前页记录条数</param>
        /// <returns></returns>
        public static IHtmlContent Pager(this IHtmlHelper htmlHelper, Http.HttpRequest request,int rowCount)
        {
            StringBuilder resultBuilder = new StringBuilder();//输出结果
            try
            {
                int pageIndex = 1;
                string url = request.Path;
                //拼接除页码参数以外的所有参数
                StringBuilder paramBuilder = new StringBuilder();
                foreach (string key in request.Query.Keys)
                {
                    if (key == "p")
                    {
                        continue;
                    }
                    if (paramBuilder.Length>0)
                    {
                        paramBuilder.Append("&");
                    }
                    paramBuilder.AppendFormat($"{key}={request.Query[key]}");
                }
                if (paramBuilder.Length > 0)
                {
                    url = $"{url}?{paramBuilder}";
                }
                //获取当前页码
                if (request.Query["p"].Count>0)
                {
                    pageIndex = int.Parse(request.Query["p"]);
                }
                //当带页码参数时的URL地址拼接
                string pageUrl = $"{url}{(paramBuilder.Length > 0 ? "&" : "?")}";
                //处理分页样式
                resultBuilder.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"{0}\">回到首页</a></li>", url);

                resultBuilder.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"{0}p={1}\">上一页</a></li>", pageUrl, pageIndex == 1 ? 1 : pageIndex - 1);

                resultBuilder.Append(string.Format("<li class=\"page-item active\"><span class=\"page-link\">{0}</span></li>", pageIndex));
                //
                if (rowCount > 0)
                {
                    resultBuilder.AppendFormat("<li class=\"page-item\"><a class=\"page-link\" href=\"{0}p={1}\">下一页</a></li>", pageUrl, pageIndex + 1);
                }
                else
                {
                    resultBuilder.AppendFormat("<li class=\"page-item\"><a class=\"page-link active\" href=\"{0}\">你已经到了一个没有任何数据的地方...</a></li>", "#");
                }
            }
            catch
            {
                resultBuilder.Append("分页出现异常...");
            }
            return new HtmlString(resultBuilder.ToString());
        }
    }
}