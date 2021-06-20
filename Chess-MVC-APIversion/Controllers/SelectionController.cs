using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess_MVC_APIversion.Controllers
{
    public class SelectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
