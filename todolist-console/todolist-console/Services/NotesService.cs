using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using todolist_console.Models;
using TextCopy;
using WindowsInput;
using WindowsInput.Native;

namespace todolist_console.Services
{
    public class NotesService
    {
        public Notes CreateNote()
        {
            Notes newNote = null;
            while(newNote == null)
            {
                Console.Write("Enter a note name => ");
                string title = Console.ReadLine();
                Console.Write("Enter a description => ");
                string description = Console.ReadLine();
                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description) && Regex.IsMatch(title, @"^[\p{L}0-9\s]+$"))
                {
                    newNote = new Notes(title, description);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
            Console.WriteLine("Note was created!");
            return newNote;
        }
        public void EditNote(Notes foundedNote)
        {
            Console.Write("What will we edit Title or Decription?" +
                    "\nEnter [0/1]: ");
            Clipboard clipboard = new Clipboard();
            int choice = Int32.Parse(Console.ReadLine());
            switch(choice)
            {
                case 0:
                    clipboard.SetText(foundedNote.Title);
                    break;
                case 1:
                    clipboard.SetText(foundedNote.Description);
                    break;
            }
            InputSimulator inputSimulator = new InputSimulator();
            var pasteTask = Task.Run(() =>
            {
                Thread.Sleep(100);
                inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
            });
            string newText = Console.ReadLine();
            pasteTask.Wait();
            if(choice == 1)
            {
                foundedNote.Description = newText;
            } else
            {
                foundedNote.Title = newText;
            }
            Console.WriteLine("Note was edited!");
        }
        public DateTime DeleteNote(Dictionary<DateTime, Notes> notes)
        {
            Notes foundedNote = FindNote(notes);
            Console.WriteLine("Note was deleted!");
            return foundedNote.Date;
        }
        public Notes FindNote(Dictionary<DateTime, Notes> notes)
        {
            Notes note = null;
            while(note == null)
            {
                Console.Write("Enter a note name for find => ");
                string foundedTitle = Console.ReadLine();
                Notes foundedNote = notes.Values.FirstOrDefault(t => t.Title == foundedTitle);
                if (foundedNote != null)
                {
                    note = foundedNote;
                }
                else
                {
                    Console.WriteLine("Task not found. Please try again.");
                }
            }
            return note;
        }
        public void CheckNote(Notes note)
        {
            int chunkSize = 35;
            List<string> chunks = new List<string>();
            for (int i = 0; i < note.Description.Length; i += chunkSize)
            {
                int length = Math.Min(chunkSize, note.Description.Length - i);
                string chunk = note.Description.Substring(i, length);
                chunks.Add(chunk);
            }
            Console.WriteLine("Title           || Description                          || Date");
            Console.WriteLine("------------------------------------------------------------------------------");
            int maxChunks = chunks.Count;
            for (int i = 0; i < maxChunks; i++)
            {
                string title = i == 0 ? note.Title : string.Empty;
                string description = i < chunks.Count ? chunks[i] : string.Empty;
                string date = i == 0 ? note.Date.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty;
                Console.WriteLine($"{title.PadRight(15)} || {description.PadRight(35)}  || {date}");
            }
            Console.WriteLine("------------------------------------------------------------------------------");
        }
        public void CheckNotes(Dictionary<DateTime, Notes> notes)
        {
            if(notes.Count == 0)
            {
                Console.WriteLine("You have no notes!");
                return;
            }
            Console.WriteLine("Title           || Description                          || Date");
            Console.WriteLine("------------------------------------------------------------------------------");
            foreach (KeyValuePair<DateTime, Notes> kvp in notes)
            {
                string title = kvp.Value.Title;
                string descr = kvp.Value.Description;
                if (kvp.Value.Title.Length > 15)
                {
                    title = kvp.Value.Title.Substring(0, 12);
                    title = title + "...";
                }
                if (kvp.Value.Description.Length > 35)
                {
                    descr = kvp.Value.Description.Substring(0, 32);
                    descr = descr + "...";
                }
                Console.WriteLine(title.PadRight(15) + " || " + descr.PadRight(35) + "  || " + kvp.Value.Date.ToString("dd/MM/yyyy HH:mm:ss"));
                Console.WriteLine("------------------------------------------------------------------------------");
            }
        }
    }
}
