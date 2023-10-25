using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todolist_console.Enums;
using todolist_console.Models;
using todolist_console.Services;

namespace todolist_console
{
    internal class NotesMenu
    {
        public void ShowMenu()
        {
            NoteMenu menu = new NoteMenu();
            Dictionary<DateTime, Notes> notes = new Dictionary<DateTime, Notes>(); 
            NotesService service = new NotesService();
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
                Console.Clear();
                switch (menu)
                {
                    case NoteMenu.CreateNote:
                        Console.WriteLine("Your choice => Create Note");
                        Notes note = service.CreateNote();
                        notes.Add(note.Date, note);
                        break;
                    case NoteMenu.EditNote:
                        Console.WriteLine("Your choice => Edit Note");
                        service.CheckNotes(notes);
                        service.EditNote(service.FindNote(notes));
                        break;
                    case NoteMenu.DeleteNote:
                        Console.WriteLine("Your choice => Delete Note");
                        service.CheckNotes(notes);
                        notes.Remove(service.DeleteNote(notes));
                        break;
                    case NoteMenu.FindNote:
                        Console.WriteLine("Your choice => Find Note");
                        service.CheckNotes(notes);
                        service.CheckNote(service.FindNote(notes));
                        break;
                    case NoteMenu.CheckNotes:
                        Console.WriteLine("Your choice => Check Notes");
                        service.CheckNotes(notes);
                        break;
                    case NoteMenu.End:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
    }
}
