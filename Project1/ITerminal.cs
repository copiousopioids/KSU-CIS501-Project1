using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class ITerminal
    {
        /// <summary>
        /// Constructs a new UserInterface object.
        /// </summary>  
        public ITerminal()
        {
            
        }

        /// <summary>
        /// Writes the string to console.
        /// </summary>
        /// <param name="s">The string to display.</param>
        public void Display(string s)
        {
            Console.Write(s);
        }

        /// <summary>
        /// Writes one line to the console.
        /// </summary>
        public void DisplayLine()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Writes the string to console and inserts a line break.
        /// </summary>
        /// <param name="s">the string to display.</param>
        public void DisplayLine(string s)
        {
            Console.WriteLine(s);
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
        public void DisplayHands(OldMaid game)
        {
            foreach (Player p in game._currentPlayers)
            {
                Console.WriteLine(p.Name + "\t: " + p.HandToString());
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
                char answer = GetChar("Do you want to play again? (Y/N)", "YNyn");
                if (answer == 'Y')
                {
                    valid = true;
                    return true;
                }
                else if (answer == 'N')
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

        /// <summary>
        /// Gets a char answer from the user.
        /// </summary>
        /// <param name="prompt">The string prompt to display to the user.</param>
        /// <param name="chars">a string of the chars that give a valid answer.</param>
        /// <returns></returns>
        public char GetChar(string prompt, string chars)
        {
            bool valid = false;
            char[] charArray = chars.ToUpper().ToCharArray();
            while (!valid)
            {
                Display(prompt);
                string answer = Console.ReadLine();
                if (answer.Length == 1)
                {
                    char answerChar = answer.ElementAt(0);
                    if (charArray.Contains(answerChar))
                    {
                        return answerChar;
                    }
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid Response.");
                }
            }
            return '~';
        }
        
        /// <summary>
        /// Gets a string answer from the user.
        /// </summary>
        /// <param name="prompt">The prompt to display.</param>
        /// <param name="length">The desired length of the string answer.</param>
        /// <returns>Returns the user's input if the size matches the length passed in.</returns>
        public string GetString(string prompt, int length)
        {
            bool valid = false;
            while (!valid)
            {
                Display(prompt);
                string answer = Console.ReadLine();

                // User must type "CR" if length = 0
                if (answer.Length == length || (length == 0 && answer == "CR"))
                {
                    return answer;
                }
                else
                {
                    valid = false;
                    Console.WriteLine("Invalid Response.");
                }
            }
            return "error";
        }

        /// <summary>
        /// Gets an int value from the user.
        /// </summary>
        /// <param name="prompt">The string prompt to display.</param>
        /// <param name="min">the minimum int value allowed.</param>
        /// <param name="max">the maximum int value allowed.</param>
        /// <returns>Returns the value selected by user.</returns>
        public int GetInt(string prompt, int min, int max)
        {
            bool isValid = false;
            while (!isValid)
            {
                try
                {
                    Console.Write(prompt + "(" + min + " - " + max + "): ");
                    int num = Convert.ToInt32(Console.ReadLine());
                    if (num < min || num > max)
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
                catch (FormatException e)
                {
                    Console.WriteLine("Invalid Input.");
                    isValid = false;
                }
            }

            return 2;
        }


        /// <summary>
        /// Wait for user. Uses default message.
        /// </summary>
        public void WaitForUser()
        {
            Console.Write("Press <Enter> to Continue... ");
            Console.Read();
        }

        /// <summary>
        /// Wait for user.
        /// </summary>
        /// <param name="prompt">String to display.</param>
        public void WaitForUser(string prompt)
        {
            Console.Write(prompt);
            Console.Read();
        }

    }
}
