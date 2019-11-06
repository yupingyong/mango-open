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

namespace Mango.Module.CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        public ContentController(IUnitOfWork<MangoDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 内容编辑
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(Models.ContentEditRequestModel requestModel)
        {
            if (!string.IsNullOrEmpty(requestModel.Title) || requestModel.Title == "")
            {
                return APIReturnMethod.ReturnFailed("标题不能为空");
            }
            if (!string.IsNullOrEmpty(requestModel.Contents) || requestModel.Contents == "")
            {
                return APIReturnMethod.ReturnFailed("内容不能为空");
            }
            //
            Entity.m_CmsContents entity = new Entity.m_CmsContents();
            entity.Contents = requestModel.Contents;//Framework.Core.HtmlFilter.SanitizeHtml(model.Contents);
            entity.LastTime = DateTime.Now;
            entity.Title = requestModel.Title;
            entity.ContentsId = requestModel.ContentsId;
            entity.ChannelId = requestModel.ChannelId;
            var repository = _unitOfWork.GetRepository<Entity.m_CmsContents>();
            repository.Update(entity);
            int resultCount = _unitOfWork.SaveChanges();
            return resultCount > 0 ? APIReturnMethod.ReturnSuccess() : APIReturnMethod.ReturnFailed();
        }
        /// <summary>
        /// 内容发布
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(Models.ContentReleaseRequestModel requestModel)
        {
            if (!string.IsNullOrEmpty(requestModel.Title)|| requestModel.Title=="")
            {
                return APIReturnMethod.ReturnFailed("标题不能为空");
            }
            if (!string.IsNullOrEmpty(requestModel.Contents) || requestModel.Contents == "")
            {
                return APIReturnMethod.ReturnFailed("内容不能为空");
            }
            //
            Entity.m_CmsContents entity = new Entity.m_CmsContents();
            entity.Contents = requestModel.Contents;//Framework.Core.HtmlFilter.SanitizeHtml(model.Contents);
            entity.ImgUrl = string.Empty;
            entity.StateCode = 1;
            entity.PostTime = DateTime.Now;
            entity.PlusCount = 0;
            entity.LastTime = DateTime.Now;
            entity.Tags = "";
            entity.ReadCount = 0;
            entity.Title = requestModel.Title;
            entity.AccountId = requestModel.AccountId;
            entity.AnswerCount = 0;
            entity.ChannelId = requestModel.ChannelId;
            var repository = _unitOfWork.GetRepository<Entity.m_CmsContents>();
            repository.Insert(entity);
            int resultCount = _unitOfWork.SaveChanges();
            return resultCount > 0 ? APIReturnMethod.ReturnSuccess() : APIReturnMethod.ReturnFailed();
        }
    }
}