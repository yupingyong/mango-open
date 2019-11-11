using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.WebSite.Models
{
    public class WebSiteNavigationDataModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int NavigationId { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>

        public string LinkUrl { get; set; }

        /// <summary>
        /// 导航名称
        /// </summary>

        public string NavigationName { get; set; }

        /// <summary>
        /// 是否为跳转到新窗口
        /// </summary>

        public bool IsTarget { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>

        public DateTime AppendTime { get; set; }

        /// <summary>
        /// 排序(从小到大)
        /// </summary>

        public int SortCount { get; set; }
        /// <summary>
        /// 是否显示到前端
        /// </summary>

        public bool IsShow { get; set; }
    }
}
