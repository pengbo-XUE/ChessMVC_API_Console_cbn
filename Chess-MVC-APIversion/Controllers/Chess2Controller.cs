using Chess_MVC_APIversion.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Chess_MVC_APIversion.Controllers
{
    public class Chess2Controller : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        bool click = Global.firstClick;

        public ActionResult HandleClick(string chess)
        {
            int newx;
            int newy;
            if (click == true)
            {
               
                Global.firstx = Int32.Parse(chess[0].ToString());
                Global.firsty = Int32.Parse(chess[1].ToString());
                if (Global.chess.game_board[Global.firstx, Global.firsty] == null)
                {
                    Global.firstClick = true;
                    Global.displayMsg = "select a piece";
                    return View("Index");
                }
                Global.firstClick = false;
                return View("Index");
            }

            else if (click == false)
            {
                newx = Int32.Parse(chess[0].ToString());
                newy = Int32.Parse(chess[1].ToString());
                Global.pipe.sendData("move" + "," + newx.ToString() + "," + newy.ToString() + "," + Global.chess.game_board[Global.firstx, Global.firsty]);
                if (Global.pipe.reciveData())
                {
                    Global.chess.game_board[newx, newy] = Global.chess.game_board[Global.firstx, Global.firsty];
                    Global.chess.game_board[Global.firstx, Global.firsty] = null;
                    Global.firstClick = true;
                    Global.displayMsg = "Good move!!";
                    return View("Index");
                }
                else
                {
                    Global.displayMsg = "Invalid move!!";
                    Global.firstClick = true;
                    return View("Index");
                }

            }
            return View("Index");
        }

        public ActionResult HandleReset() 
        {
            Chess newChess = new Chess();
            Global.chess = newChess;
            Global.pipe.sendData("reset");
            Global.pipe.reciveData();
            return View("Index");
        }

        public IActionResult ToHome()
        {
            return View("Chess2Home");
        }
        public ActionResult StartChess()
        {
            Global.clearInput();
            //HandleReset();
            return RedirectToAction("Index", "Chess2");
        }
    }
}
