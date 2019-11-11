using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mango.Module.Core.Entity;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;
using Mango.Framework.Services.Cache;
using Newtonsoft.Json;
namespace Mango.Module.WebSite.Controllers
{
    [Area("WebSite")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        private ICacheService _cacheService;
        public BaseController(IUnitOfWork<MangoDbContext> unitOfWork,ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }
        [HttpGet("navigation")]
        public IActionResult GetNavigation()
        {
            string cacheData = _cacheService.Get("WebSiteNavigationCache");
            if (string.IsNullOrEmpty(cacheData))
            {
                var repository = _unitOfWork.GetRepository<Entity.m_WebSiteNavigation>();
                var resultData= repository.Query()
                .OrderBy(nav => nav.SortCount)
                .Where(q=>q.IsShow==true)
                .Select(nav => new Models.WebSiteNavigationDataModel()
                {
                    AppendTime = nav.AppendTime.Value,
                    IsShow = nav.IsShow.Value,
                    IsTarget = nav.IsTarget.Value,
                    LinkUrl = nav.LinkUrl,
                    NavigationId = nav.NavigationId.Value,
                    NavigationName = nav.NavigationName,
                    SortCount = nav.SortCount.Value
                }).ToList();
                cacheData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resultData)));
                //写入缓存
                _cacheService.Add("WebSiteNavigationCache", cacheData);
                return APIReturnMethod.ReturnSuccess(resultData);
            }
            else
            {
                cacheData = Encoding.UTF8.GetString(Convert.FromBase64String(cacheData.Replace("\"", "")));
                //从缓存中获取
                return APIReturnMethod.ReturnSuccess(JsonConvert.DeserializeObject<List<Models.WebSiteNavigationDataModel>>(cacheData));
            }
        }
        
        [HttpGet("config")]
        public IActionResult GetConfig()
        {
            string cacheData = _cacheService.Get("WebSiteConfigCache");
            if (string.IsNullOrEmpty(cacheData))
            {
                var repository = _unitOfWork.GetRepository<Entity.m_WebSiteConfig>();
                var resultData = repository.Query()
                    .OrderBy(cfg => cfg.ConfigId)
                    .Select(cfg => new Models.WebSiteConfigResultModel()
                    {
                        ConfigId = cfg.ConfigId.Value,
                        CopyrightText = cfg.CopyrightText,
                        FilingNo = cfg.FilingNo,
                        IsLogin = cfg.IsLogin.Value,
                        IsRegister = cfg.IsRegister.Value,
                        WebSiteDescription = cfg.WebSiteDescription,
                        WebSiteKeyWord = cfg.WebSiteKeyWord,
                        WebSiteName = cfg.WebSiteName,
                        WebSiteTitle = cfg.WebSiteTitle,
                        WebSiteUrl = cfg.WebSiteUrl
                    })
                    .FirstOrDefault();
                cacheData = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(resultData)));
                //写入缓存
                _cacheService.Add("WebSiteConfigCache", cacheData);
                return APIReturnMethod.ReturnSuccess(resultData);
            }
            else
            {
                cacheData = Encoding.UTF8.GetString(Convert.FromBase64String(cacheData.Replace("\"", "")));
                return APIReturnMethod.ReturnSuccess(JsonConvert.DeserializeObject<Models.WebSiteConfigResultModel>(cacheData));
            }
        }
    }
}
