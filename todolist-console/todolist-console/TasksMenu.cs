using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todolist_console.Models;
using todolist_console.Enums;
using todolist_console.Services;

namespace todolist_console
{
    internal class TasksMenu
    {
        public void ShowMenu()
        {
            TaskMenu menu = new TaskMenu();
            List<Tasks> tasks = new List<Tasks>();
            TaskService service = new TaskService();
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
                        Console.WriteLine("Your choice => Create Task");
                        tasks.Add(service.CreateTask());
                        break;
                    case TaskMenu.EditTask:
                        Console.WriteLine("Your choice => Edit Task");
                        service.CheckTasks(tasks);
                        service.EditTask(service.FindTask(tasks));
                        break;
                    case TaskMenu.DeleteTask:
                        Console.WriteLine("Your choice => Delete Task");
                        service.CheckTasks(tasks);
                        tasks.Remove(service.FindTask(tasks));
                        Console.WriteLine("Task was deleted!");
                        break;
                    case TaskMenu.CheckTasks:
                        service.CheckTasks(tasks);
                        break;
                    case TaskMenu.End:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadLine();
            }
        }
    }
}
