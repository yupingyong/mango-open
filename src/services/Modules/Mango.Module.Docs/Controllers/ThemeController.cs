using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mango.Module.Core.Entity;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;

namespace Mango.Module.Docs.Controllers
{
    [Area("Docs")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        public ThemeController(IUnitOfWork<MangoDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("document/{id}")]
        public IActionResult Get([FromRoute]int id,bool isGood)
        {
            var docRepository = _unitOfWork.GetRepository<Entity.m_Docs>();
            var docListData = docRepository.Query()
                    .Where(q => q.ThemeId == id && q.IsShow == true)
                    .OrderByDescending(q => q.DocsId)
                    .Select(q => new Models.DocumentDataModel()
                    {
                        DocsId = q.DocsId.Value,
                        ShortTitle = q.ShortTitle,
                        Title = q.Title,
                        ThemeId = q.ThemeId.Value,
                        IsShow = q.IsShow.Value
                    })
                    .ToList();
            return APIReturnMethod.ReturnSuccess(docListData);
        }
        /// <summary>
        /// 获取文档主题列表
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        [HttpGet("{p}")]
        public IActionResult Get([FromRoute]int p)
        {
            var repository = _unitOfWork.GetRepository<Entity.m_DocsTheme>();
            var accountRepository = _unitOfWork.GetRepository<m_Account>();
            var resultData = repository.Query()
                .Join(accountRepository.Query(), t => t.AccountId, acc => acc.AccountId, (t, acc) => new Models.ThemeDataModel()
                {
                    ThemeId = t.ThemeId.Value,
                    HeadUrl = acc.HeadUrl,
                    IsShow = t.IsShow.Value,
                    LastTime = t.LastTime.Value,
                    PlusCount = t.PlusCount.Value,
                    NickName = acc.NickName,
                    AppendTime = t.AppendTime.Value,
                    ReadCount = t.ReadCount.Value,
                    Title = t.Title,
                    Tags = t.Tags,
                    AccountId = t.AccountId.Value
                })
                .OrderByDescending(q => q.ThemeId)
                .Skip(10 * (p - 1))
                .Take(10)
                .ToList();
            return APIReturnMethod.ReturnSuccess(resultData);
        }
    }
}