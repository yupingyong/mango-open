using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.Common
{
    /// <summary>
    /// API服务器请求地址配置类
    /// </summary>
    public class ApiServerConfig
    {
        #region 账号中心接口
        /// <summary>
        /// 账号登录接口
        /// </summary>
        public static string Account_Login { get; set; } = "/api/Account/Login";
        #endregion
        #region 网址导航接口
        /// <summary>
        /// 根据指定条件获取数据
        /// </summary>
        public static string Navigation_GetNavigationByCondition { get; set; } = "/api/Navigation/GetNavigationByCondition";
        /// <summary>
        /// 获取导航数据
        /// </summary>
        public static string Navigation_GetNavigationList { get; set; } = "/api/Navigation/GetNavigationList";
        /// <summary>
        /// 获取导航分类
        /// </summary>
        public static string Navigation_GetNavigationClassify { get; set; } = "/api/Navigation/GetNavigationClassify";
        #endregion
        #region 文档模块接口
        /// <summary>
        /// 根据文档ID获取文档数据
        /// </summary>
        public static string Docs_GetDocsById { get; set; } = "/api/Docs/GetDocsById";
        /// <summary>
        /// 根据文档主题ID获取文档主题数据
        /// </summary>
        public static string Docs_GetDocsThemeById { get; set; } = "/api/Docs/GetDocsThemeById";
        /// <summary>
        /// 根据文档主题ID获取文档列表
        /// </summary>
        public static string Docs_GetDocsList { get; set; } = "/api/Docs/GetDocsList";
        /// <summary>
        /// 获取文档主题列表
        /// </summary>
        public static string Docs_GetDocsThemeList { get; set; } = "/api/Docs/GetDocsThemeList";
        #endregion
        #region CMS模块接口
        /// <summary>
        /// 获取帖子回复列表数据
        /// </summary>
        public static string Posts_GetPostsAnswerList { get; set; } = "/api/Posts/GetPostsAnswerList";
        /// <summary>
        /// 获取CMS文章列表(/api/CMS/Content/{channelId}/{p})
        /// </summary>
        public static string CMS_ContentsList { get; set; } = "/api/CMS/Content";
        /// <summary>
        /// 获取CMS文章接口(/api/CMS/Content/{contentsId})
        /// </summary>
        public static string CMS_Contents { get; set; } = "/api/CMS/Content";
        /// <summary>
        /// 获取自定义类型的数据(/api/CMS/Content/customize/{type}/{count})
        /// </summary>
        public static string CMS_GetContentsCustomizeList { get; set; } = "/api/CMS/Content/customize";
        /// <summary>
        /// 获取帖子列表数据
        /// </summary>
        public static string CMS_ReleaseContent { get; set; } = "/api/CMS/Content";
        /// <summary>
        /// 获取频道数据
        /// </summary>
        public static string CMS_GetChannel{ get; set; } = "/api/CMS/Channel";
        #endregion
        #region 公共数据接口
        /// <summary>
        /// 获取网站顶部导航数据接口
        /// </summary>
        public static string WebSite_GetWebSiteNavigation { get; set; } = "/api/WebSite/Base/navigation";
        /// <summary>
        /// 获取网站基本设置数据接口
        /// </summary>
        public static string WebSite_GetWebSiteConfig { get; set; } = "/api/WebSite/Base/config";
        #endregion
    }
}
