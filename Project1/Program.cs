/// CIS 501 - Kansas State University
/// Zach Marcolesco
/// Project 0 - Old Maid
/// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_0
{
    class Program
    {
        static void Main(string[] args)
        {
            bool playAgain = true;

            while (playAgain == true)
            {
                Game _game = new Game();

                _game.PlayGame();

                playAgain = _game._gameUI.PlayAgain();
            }

        }
      
    }
}
