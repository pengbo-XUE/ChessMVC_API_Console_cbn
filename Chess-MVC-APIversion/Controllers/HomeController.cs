using Chess_MVC_APIversion.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chess_MVC_APIversion.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult StartChess()
        {
           
            
            return RedirectToAction("Index", "Chess");
        }
    }
}
