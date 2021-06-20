
using Microsoft.AspNetCore.Mvc;
using System;

namespace Chess_MVC_APIversion.Models
{
    /// <summary>
    /// The Chess  class.
    /// Contains all methods and property for the chess game.
    /// </summary>
    public class Chess
    {
        /// <summary>
        /// class Board.
        /// 2d array representing a chessboard.
        /// </summary>
        public string[,] game_board = new string[8, 8];
        /// <summary>
        /// class constructor.
        /// populates the game board.
        /// </summary>
        public Chess()
        {
            this.game_board[3, 0] = "bkk";
            this.game_board[3, 7] = "wkk";

            //QUEEN
            this.game_board[4, 0] = "bq";
            this.game_board[4, 7] = "wq";


            //ROOKS
            this.game_board[0, 0] = "br1";
            this.game_board[7, 0] = "br2";
            this.game_board[0, 7] = "wr1";
            this.game_board[7, 7] = "wr2";

            //KNIGHTS
            this.game_board[1, 0] = "bk1";
            this.game_board[6, 0] = "bk2";
            this.game_board[1, 7] = "wk1";
            this.game_board[6, 7] = "wk2";

            //BISHOPS
            this.game_board[2, 0] = "bb1";
            this.game_board[5, 0] = "bb2";
            this.game_board[2, 7] = "wb1";
            this.game_board[5, 7] = "wb2";



            //BLACK PAWN
            this.game_board[0, 1] = "bp1";
            this.game_board[1, 1] = "bp2";
            this.game_board[2, 1] = "bp3";
            this.game_board[3, 1] = "bp4";
            this.game_board[4, 1] = "bp5";
            this.game_board[5, 1] = "bp6";
            this.game_board[6, 1] = "bp7";
            this.game_board[7, 1] = "bp8";
            //WHITE PAWN
            this.game_board[0, 6] = "wp1";
            this.game_board[1, 6] = "wp2";
            this.game_board[2, 6] = "wp3";
            this.game_board[3, 6] = "wp4";
            this.game_board[4, 6] = "wp5";
            this.game_board[5, 6] = "wp6";
            this.game_board[6, 6] = "wp7";
            this.game_board[7, 6] = "wp8";
        }
        /// <summary>
        /// A method that moves the pieces on the game board based on user input and API returned results
        /// </summary>
        /// <param name="click">a boolean var indicating weather the click is first click or second</param>
        /// <param name="chess">the instruction string passed from the user </param>
        /// <example>
        /// <code>
        ///   public ActionResult HandleClick(string chess)
        ///   {
        ///       Global.chess.movepiece(click, chess);
        ///       return View("Index");
        ///    }
        /// </code>
        /// </example>
        public bool movepiece(bool click, string chess)
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
                    return true;
                }
                Global.firstClick = false;
                return false;
            }

            else if (click == false)
            {
                newx = Int32.Parse(chess[0].ToString());
                newy = Int32.Parse(chess[1].ToString());
                string response = Global.sendJSON("move", newx, newy, Global.chess.game_board[Global.firstx, Global.firsty]);
                if (response != null)
                {
                    if (response == "moved")
                    {
                        if (Global.chess.game_board[newx, newy] != null)
                        {
                            Global.caped.Add(Global.chess.game_board[newx, newy]);
                        }
                        Global.chess.game_board[newx, newy] = Global.chess.game_board[Global.firstx, Global.firsty];
                        Global.chess.game_board[Global.firstx, Global.firsty] = null;
                        Global.firstClick = true;
                        Global.displayMsg = "Good move!!";
                        return true;
                    }
                    Global.displayMsg = "Invalid move!!";
                }
                else
                {
                    Global.displayMsg = "Invalid move!!";
                    Global.firstClick = true;
                    return true;
                }

            }
            return false;
        }
        /// <summary>
        /// A method that resets the game
        /// </summary>
        /// <example>
        /// <code>
        ///public ActionResult HandleReset()
        /// {
        ///      Global.chess.reset();
        ///     return View("Index");
        ///  }
        /// </code>
        /// </example>
    public void reset() 
        {
            Chess newChess = new Chess();
            Global.chess = newChess;
            Global.sendJSON("reset");
        }

    }
}

