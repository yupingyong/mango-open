using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mango.Module.Core.Entity;
using Mango.Framework.Infrastructure;
using Mango.Framework.Data;

namespace Mango.Module.Account.Controllers
{
    [Area("Account")]
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private IUnitOfWork<MangoDbContext> _unitOfWork;
        public InformationController(IUnitOfWork<MangoDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 更新用户资料
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(Models.InformationUpdateRequestModel requestModel)
        {
            if (string.IsNullOrEmpty(requestModel.InformationValue))
            {
                return APIReturnMethod.ReturnFailed("请输入待修改项的值!");
            }
            var repository = _unitOfWork.GetRepository<m_Account>();
            var accountData = repository.Query().Where(q => q.AccountId == requestModel.AccountId).FirstOrDefault();
            switch (requestModel.InformationType)
            {
                case 3:
                    accountData.Tags = requestModel.InformationValue;
                    break;
                case 4:
                    accountData.AddressInfo = requestModel.InformationValue;
                    break;
                case 5:
                    accountData.Sex = requestModel.InformationValue;
                    break;
                case 1:
                    accountData.NickName = requestModel.InformationValue;
                    break;
                case 2:
                    accountData.HeadUrl = requestModel.InformationValue;
                    break;
            }
            repository.Update(accountData);
            var resultCount = _unitOfWork.SaveChanges();
            return resultCount > 0 ? APIReturnMethod.ReturnSuccess() : APIReturnMethod.ReturnFailed();
        }
    }
}