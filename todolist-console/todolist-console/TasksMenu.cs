using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolist_console.Enums;

namespace todolist_console
{
    internal class TasksMenu
    {
        public void ShowMenu()
        {
            TaskMenu menu = new TaskMenu();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Tasks menu:" +
                        "\n1 - Create Task" +
                        "\n2 - Edit Task" +
                        "\n3 - Delete Task" +
                        "\n4 - Check Tasks" +
                        "\n5 - Return to the main menu");
                Console.Write("Input a choice: ");
                Enum.TryParse(Console.ReadLine(), out menu);
                switch (menu)
                {
                    case TaskMenu.CreateTask:
                        Console.WriteLine("1");
                        Console.ReadKey();
                        break;
                    case TaskMenu.EditTask:
                        Console.WriteLine("2");
                        Console.ReadKey();
                        break;
                    case TaskMenu.DeleteTask:
                        Console.WriteLine("3");
                        Console.ReadKey();
                        break;
                    case TaskMenu.CheckTasks:
                        Console.WriteLine("4");
                        Console.ReadKey();
                        break;
                    case TaskMenu.End:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.\n" +
                                          "Press any key to continue.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
