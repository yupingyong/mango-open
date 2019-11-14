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
    [Route("api/[controller]")]
    [ApiController]
    public class ContentsController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        public ContentsController(IUnitOfWork<MangoDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 根据文档主题ID获取主题详情数据
        /// </summary>
        /// <param name="themeId">文档主题ID</param>
        /// <param name="docsId">文档ID</param>
        /// <returns></returns>
        [HttpGet("{themeId}/{docsId}")]
        public IActionResult Get(int themeId,int docsId)
        {
            var accountRepository = _unitOfWork.GetRepository<m_Account>();
            var docRepository = _unitOfWork.GetRepository<Entity.m_Docs>();
            var themeRepository = _unitOfWork.GetRepository<Entity.m_DocsTheme>();
            var docListData = docRepository.Query()
                    .Where(q => q.ThemeId == themeId && q.IsShow == true)
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
            var docsContentsData = docRepository.Query()
                           .Join(accountRepository.Query(), doc => doc.AccountId, acc => acc.AccountId, (doc, acc) => new Models.DocsContentsModel()
                           {
                               DocsId = doc.DocsId.Value,
                               HeadUrl = acc.HeadUrl,
                               IsShow = doc.IsShow.Value,
                               LastTime = doc.LastTime.Value,
                               PlusCount = doc.PlusCount.Value,
                               NickName = acc.NickName,
                               AppendTime = doc.AppendTime.Value,
                               ReadCount = doc.ReadCount.Value,
                               Title = doc.Title,
                               Tags = doc.Tags,
                               AccountId = doc.AccountId.Value,
                               ShortTitle = doc.ShortTitle,
                               ThemeId = doc.ThemeId.Value,
                               Contents = doc.Contents,
                               IsAudit = doc.IsAudit.Value
                           })
                           .Where(q => q.DocsId == docsId)
                           .FirstOrDefault();
            if (docsContentsData != null)
            {
                //更新浏览次数
                _unitOfWork.DbContext.MangoUpdate<Entity.m_Docs>(q => q.ReadCount == q.ReadCount + 1, q => q.DocsId == docsId);
            }
            return APIReturnMethod.ReturnSuccess(new { DocsContentsData = docsContentsData, DocListData = docListData });
        }
        /// <summary>
        /// 根据文档主题ID获取主题详情数据
        /// </summary>
        /// <param name="themeId">文档主题ID</param>
        /// <returns></returns>
        [HttpGet("{themeId}")]
        public IActionResult Get(int themeId)
        {
            var accountRepository = _unitOfWork.GetRepository<m_Account>();
            var docRepository = _unitOfWork.GetRepository<Entity.m_Docs>();
            var themeRepository = _unitOfWork.GetRepository<Entity.m_DocsTheme>();
            var docListData = docRepository.Query()
                    .Where(q => q.ThemeId == themeId && q.IsShow == true)
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
            var themeData = themeRepository.Query()
                .Join(accountRepository.Query(), doc => doc.AccountId, acc => acc.AccountId, (doc, acc) => new Models.ThemeDataModel()
                {
                    ThemeId = doc.ThemeId.Value,
                    HeadUrl = acc.HeadUrl,
                    IsShow = doc.IsShow.Value,
                    LastTime = doc.LastTime.Value,
                    PlusCount = doc.PlusCount.Value,
                    NickName = acc.NickName,
                    AppendTime = doc.AppendTime.Value,
                    ReadCount = doc.ReadCount.Value,
                    Title = doc.Title,
                    Tags = doc.Tags,
                    AccountId = doc.AccountId.Value,
                    Contents = doc.Contents
                })
               .Where(q => q.ThemeId == themeId)
               .OrderByDescending(q => q.ThemeId)
               .FirstOrDefault();
            if (themeData != null)
            {
                //更新浏览次数
                _unitOfWork.DbContext.MangoUpdate<Entity.m_DocsTheme>(q => q.ReadCount == q.ReadCount + 1, q => q.ThemeId == themeId);
            }
            return APIReturnMethod.ReturnSuccess(new { ThemeData= themeData ,DocListData= docListData });
        }
    }
}