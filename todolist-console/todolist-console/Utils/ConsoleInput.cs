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
