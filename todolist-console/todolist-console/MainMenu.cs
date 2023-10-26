using todolist_console.Enums;
using todolist_console.Menus;

namespace todolist_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            while(!exit)
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
                        TasksMenu tasksMenu = new TasksMenu();
                        tasksMenu.ShowMenu();
                        break;
                    case Menu.OpenNotesMenu:
                        NotesMenu notesMenu = new NotesMenu();
                        notesMenu.ShowMenu();
                        break;
                    case Menu.End:
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