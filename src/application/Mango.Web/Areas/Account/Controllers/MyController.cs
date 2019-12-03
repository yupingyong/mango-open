using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Areas.Account.Controllers
{
    [Area("Account")]
    public class MyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Article()
        {
            return View();
        }
        public IActionResult Theme()
        {
            return View();
        }
        public IActionResult Document()
        {
            return View();
        }
    }
}