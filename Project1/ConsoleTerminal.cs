/// When creating functions, don't say public/private
/// public interface ITerminal
/// Change this class to ConsoleTerminal, inherits iterminal
/// Make new class Interface

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    class ConsoleTerminal : ITerminal
    {
        /// <summary>
        /// Constructs a new UserInterface object.
        /// </summary>  
        public ConsoleTerminal()
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

        public char GetChar(string prompt, string chars)
        {
            bool valid = false;
            char[] charArray = chars.ToCharArray();
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
    }
}
