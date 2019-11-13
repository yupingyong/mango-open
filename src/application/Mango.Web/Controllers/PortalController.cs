using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace Mango.Web.Controllers
{
    public class PortalController : Controller
    {
        public IActionResult Index()
        {
            
            return View();
        }
    }
}