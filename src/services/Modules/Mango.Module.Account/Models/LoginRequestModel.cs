using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Module.Account.Models
{
    public class LoginRequestModel
    {
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string AccountName { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string Password { get; set; }
    }
}
