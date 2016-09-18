using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_0
{
    class UserInterface
    {
        /// <summary>
        /// Constructs a new UserInterface object.
        /// </summary>
        public UserInterface()
        {
            
        }

        /// <summary>
        /// Gets the number of AI players.
        /// </summary>
        /// <returns>An int giving the total number of AI players.</returns>
        public int GetNumAI()
        {
            bool isValid = false;
            while (!isValid)
            {
                try {
                    Console.Write("Input number of computer players (2 - 5): ");
                    int num = Convert.ToInt32(Console.ReadLine());
                    if (num < 2 || num > 5)
                    {
                        Console.Write("Invalid input.");
                        isValid = false;
                    }
                    else
                    {
                        isValid = true;
                        return num;
                    }
                }
                catch(FormatException e)
                {
                    Console.WriteLine("Invalid Input.");
                    isValid = false;
                }
            }

            return 2;
        }

        /// <summary>
        /// Displays all the player's hands.
        /// </summary>
        /// <param name="game"></param>
        public void DisplayHands(Game game)
        {
            foreach (Player p in game._playerList)
            {
                Console.WriteLine(p._name + " : " + p.HandToString());
            }
            
            //List<string> handList = new List<string>();
            //foreach (Player p in game._playerList)
            //{
            //    if (p != null && !p._isFinished)
            //    {
            //        handList.Add(p.HandToString());
            //    }
            //}
            //Console.WriteLine(game._playerList[0]._name + "    : " + handList[0]);
            //for (int i = 1; i < handList.Count; i++)
            //{
            //    Console.WriteLine(game._playerList[i]._name + " : " + handList[i]);
            //}
        }

        /// <summary>
        /// Determines if the player would like to play again.
        /// </summary>
        /// <returns>A bool indicating whether the user would like to play again.</returns>
        public bool PlayAgain()
        {
            bool valid = false;
            while (!valid)
            {
                Console.Write("Do you want to play again? (Y/N)");
                string answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                {
                    valid = true;
                    return true;
                }
                else if (answer == "N" || answer == "n")
                {
                    valid = true;
                    return false;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid Response.");
                }
            }
            return false;
        }
    }
}
