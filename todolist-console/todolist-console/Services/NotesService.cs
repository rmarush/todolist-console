using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TextCopy;
using WindowsInput;
using WindowsInput.Native;
using todolist_console.Models;
using todolist_console.Utils;

namespace todolist_console.Services
{
    public class NotesService
    {
        private readonly int _maxTitleLenght = 15;
        private readonly int _maxDescrLenght = 35;
        private readonly InputSimulator _inputSimulator = new InputSimulator();
        private readonly Clipboard _clipboard = new Clipboard();
        public Notes CreateNote()
        {
            Notes newNote = null;
            while(newNote == null)
            {
                Console.Write("Enter a note name => ");
                string title = Console.ReadLine();
                Console.Write("Enter a description => ");
                string description = Console.ReadLine();
                if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description) && Regex.IsMatch(title, RegexConstants.TitlePattern))
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
            int choice = Int32.Parse(Console.ReadLine());
            _clipboard.SetText(choice == 0 ? foundedNote.Title : foundedNote.Description);
            _inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
            string newText = Console.ReadLine();
            _ = choice == 1 ? (foundedNote.Description = newText) : (foundedNote.Title = newText);
            Console.WriteLine("Note was edited!");
        }
        public int DeleteNote(Dictionary<int, Notes> notes)
        {
            Notes foundedNote = FindNote(notes);
            return foundedNote.Date.GetHashCode();
        }
        public Notes FindNote(Dictionary<int, Notes> notes)
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
                Console.WriteLine($"{title.PadRight(_maxTitleLenght)} || {description.PadRight(_maxDescrLenght)}  || {date}");
            }
            Console.WriteLine("------------------------------------------------------------------------------");
        }
        public void CheckNotes(Dictionary<int, Notes> notes)
        {
            if(notes.Count == 0)
            {
                Console.WriteLine("You have no notes!");
                return;
            }
            Console.WriteLine("Title           || Description                          || Date");
            Console.WriteLine("------------------------------------------------------------------------------");
            foreach (var kvp in notes)
            {
                string title = kvp.Value.Title;
                string descr = kvp.Value.Description;
                if (kvp.Value.Title.Length > _maxTitleLenght)
                {
                    title = kvp.Value.Title.Substring(0, 12);
                    title = title + "...";
                }
                if (kvp.Value.Description.Length > _maxDescrLenght)
                {
                    descr = kvp.Value.Description.Substring(0, 32);
                    descr = descr + "...";
                }
                Console.WriteLine(title.PadRight(_maxTitleLenght) + " || " + descr.PadRight(_maxDescrLenght) + "  || " + kvp.Value.Date.ToString("dd/MM/yyyy HH:mm:ss"));
                Console.WriteLine("------------------------------------------------------------------------------");
            }
        }
    }
}
