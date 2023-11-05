using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todolist_console.Models;
using todolist_console.Enums;
using todolist_console.Services;
using todolist_console.Menus.Interfaces;
using System.ComponentModel.DataAnnotations;
using todolist_console.Utils;

namespace todolist_console.Menus
{
    internal class TasksMenu : IMenu
    {
        public void ShowMenu()
        {
            var service = new TaskService();
            var tasks = JsonService.LoadData<DoublyLinkedList<Tasks>>("TaskData.json");
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Tasks menu:" +
                        "\n1 - Create Task" +
                        "\n2 - Check Task by one" +
                        "\n3 - Check Tasks" +
                        "\n4 - Send to Mail" +
                        "\n5 - Return to the main menu");
                Console.Write("Input a choice: ");
                Enum.TryParse(Console.ReadLine(), out TaskMenu menu);
                Console.Clear();
                switch (menu)
                {
                    case TaskMenu.CreateTask:
                        Console.WriteLine("Your choice => Create Task");
                        tasks.Add(service.CreateTask());
                        break;
                    case TaskMenu.CheckByOne:
                        service.CheckTaskByOne(tasks);
                        break;
                    case TaskMenu.CheckTasks:
                        service.CheckTasks(tasks);
                        break;
                    case TaskMenu.EmailSend:
                        service.EmailSend(tasks);
                        break;
                    case TaskMenu.End:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            JsonService.WriteData<DoublyLinkedList<Tasks>>(tasks, "TaskData.json");

        }
    }
}
