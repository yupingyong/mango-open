using System;
using System.Collections.Generic;
using System.Text;

namespace Mango.Framework.Services.EMail
{
    public interface IEMailService
    {
        bool SendEmail(string email, string subject, string message);
    }
}
