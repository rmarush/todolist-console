using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolist_console.Enums;

namespace todolist_console
{
    internal class NotesMenu
    {
        public void ShowMenu()
        {
            Console.WriteLine("Notes Menu");
            NoteMenu menu = new NoteMenu();
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Notes menu:" +
                        "\n1 - Create Note" +
                        "\n2 - Edit Note" +
                        "\n3 - Delete Note" +
                        "\n4 - Find Note" +
                        "\n5 - Check Notes" +
                        "\n6 - Return to the main menu");
                Console.Write("Input a choice: ");
                Enum.TryParse(Console.ReadLine(), out menu);
                switch (menu)
                {
                    case NoteMenu.CreateNote:
                        Console.WriteLine("1");
                        Console.ReadKey();
                        break;
                    case NoteMenu.EditNote:
                        Console.WriteLine("2");
                        Console.ReadKey();
                        break;
                    case NoteMenu.DeleteNote:
                        Console.WriteLine("3");
                        Console.ReadKey();
                        break;
                    case NoteMenu.FindNote:
                        Console.WriteLine("4");
                        Console.ReadKey();
                        break;
                    case NoteMenu.CheckNotes:
                        Console.WriteLine("5");
                        Console.ReadKey();
                        break;
                    case NoteMenu.End:
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
