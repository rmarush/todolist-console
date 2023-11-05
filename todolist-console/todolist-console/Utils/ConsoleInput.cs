using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolist_console.Utils.Interfaces;

namespace todolist_console.Utils
{
    public class ConsoleInput : IConsoleInput
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
