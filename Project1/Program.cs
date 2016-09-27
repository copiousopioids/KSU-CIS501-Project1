/// CIS 501 - Kansas State University
/// Zach Marcolesco
/// Project 1 - Old Maid
/// 
/// Uses Knuth's Shuffle Algorithm <http://rosettacode.org/wiki/Knuth_shuffle>
///  Questions
///  - How to reuse Player objects? How to make sure arrays for hand a long enough?
///  
/// When playing again, do not reset number of players
/// GetString("message", 0) to wait for user
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Project_1
{
    class Program
    {

        static void Main(string[] args)
        {
            OldMaid _game = new OldMaid();
            _game.PlayGame();
        }
      
    }
}
