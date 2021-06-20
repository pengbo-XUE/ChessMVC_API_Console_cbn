using Chess_MVC_APIversion.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Chess_MVC_APIversion.Controllers
{

    public class ChessController : Controller
    {
        
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
        bool click = Global.firstClick;
        
        public ActionResult HandleClick(string chess)
        {
            Global.chess.movepiece(click,chess);
            return View("Index");
        }

        public ActionResult HandleReset()
        {
            Global.chess.reset();
            return View("Index");
        }
      
        public IActionResult ToHome() 
        {
            return View("ChessHome");
        }
        
        public ActionResult StartChess()
        {

            Global.clearInput();
            //HandleReset();
            return RedirectToAction("Index", "Chess");
        }
    }
}
