using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Mango.Module.Core.Entity;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;
namespace Mango.Module.Account.Controllers
{
    [Area("Account")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class PasswordController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        public PasswordController(IUnitOfWork<MangoDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 更新登录密码
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(Models.PasswordUpdateRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.Password))
            {
                return APIReturnMethod.ReturnFailed("请输入您的原登录密码!");
            }
            if (string.IsNullOrEmpty(requestModel.NewPassword))
            {
                return APIReturnMethod.ReturnFailed("请输入您的新登录密码!");
            }
            var repository = _unitOfWork.GetRepository<m_Account>();
            var accountData = repository.Query().Where(q => q.AccountId == requestModel.AccountId&&q.Password==TextHelper.MD5Encrypt(requestModel.Password)).FirstOrDefault();
            if (accountData == null)
            {
                return APIReturnMethod.ReturnFailed("请输入正确的原登录密码!");
            }
            accountData.Password = TextHelper.MD5Encrypt(requestModel.NewPassword);
            repository.Update(accountData);
            var resultCount = _unitOfWork.SaveChanges();
            return resultCount > 0 ? APIReturnMethod.ReturnSuccess() : APIReturnMethod.ReturnFailed();
        }
    }
}