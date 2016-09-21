/// CIS 501 - Kansas State University
/// Zach Marcolesco
/// Project 1 - Old Maid
/// TODO
///  - Use Arrays for conversion to enum -> int -> string
///  - Add property "FaceUp" (bool) to indicate if card is face up/down
///  - Fix all properties to use auto implement
///  - Objects must be reused (except string objects)
///  - Knuth's shuffle Algorithm (don't forget link in comments)
///  - use "DEBUG" input to switch between debug and release
///     - See codeHints
///  - All I/O using Console into ConsoleTerminal class
///  - PlayingCard (Fields: Suit, Rank, FaceUp) (ToString() should display "XX" or "suit-rank"
///  - CardDeck
///     - See Slides...
///  - Player
///     - See Slides...
///  - ComputerPlayer and HumanPlayer (inherits player)
///     - Remove duplicate pairs using array thing
///  - OldMaid
///  - ITerminal
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
