using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_1
{
    public interface ITerminal
    {
        void Display(string s);

        void DisplayLine(string s);

        void DisplayLine();

        char GetChar(string prompt, string chars);

        string GetString(string prompt, int length);

        int GetInt(string prompt, int min, int max);
    }
}
