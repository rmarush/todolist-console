﻿using todolist_console.Enums;
using todolist_console.Menus;
using todolist_console.Menus.Interfaces;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace todolist_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;   
            IMenu newMenu = null;
            var exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Main menu:" +
                        "\n1 - Open Tasks menu" +
                        "\n2 - Open Notes menu" +
                        "\n3 - End of the program");
                Console.Write("Input a choice: ");
                Enum.TryParse(Console.ReadLine(), out Menu menu);
                switch (menu)
                {
                    case Menu.OpenTasksMenu:
                        newMenu = new TasksMenu();
                        break;

                    case Menu.OpenNotesMenu:
                        newMenu = new NotesMenu();
                        break;

                    case Menu.End:
                        newMenu = null;
                        exit = true;
                        break;

                    default:
                        newMenu = null;
                        Console.WriteLine("Invalid choice. Please try again.\n" +
                                          "Press any key to continue.");
                        Console.ReadLine();
                        break;
                }
                if(newMenu != null)
                    newMenu.ShowMenu();
            }
        }
    }
}