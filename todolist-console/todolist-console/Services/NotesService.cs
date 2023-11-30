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
using System.Data.SqlTypes;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Collections;
using log4net;
using log4net.Config;
using todolist_console.Utils.Interfaces;

namespace todolist_console.Services
{
    public class NotesService
    {
        private readonly int _maxTitleLenght = 15;
        private readonly int _maxDescrLenght = 35;
        private readonly InputSimulator _inputSimulator = new InputSimulator();
        private readonly Clipboard _clipboard = new Clipboard();
        private readonly ILog log = LogManager.GetLogger(typeof(NotesService));
        private readonly IConsoleInput _consoleInput;
        
        public NotesService() {}
        public NotesService(IConsoleInput consoleInput)
        {
            _consoleInput = consoleInput;
        }

        public Notes CreateNote()
        {
            Notes newNote = null;
            while(newNote == null)
            { 
                Console.Write("Enter a note name => ");
                var title = _consoleInput.ReadLine();
                Console.Write("Enter a description => ");
                var description = _consoleInput.ReadLine();
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
            if (foundedNote == null || !ReviewNote(foundedNote))
            {
                Console.WriteLine("Editing is canceling");
                return;
            }
            Console.Write("What will we edit Title or Decription?" +
                    "\nEnter [0/1]: ");
            var choice = Int32.Parse(_consoleInput.ReadLine());
            _clipboard.SetText(choice == 0 ? foundedNote.Title : foundedNote.Description);
            _inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_V);
            var newText = _consoleInput.ReadLine();
            _ = choice == 1 ? (foundedNote.Description = newText) : (foundedNote.Title = newText);
            Console.WriteLine("Note was edited!");
        }
        public int DeleteNote(Dictionary<int, Notes> notes)
        {
            if (notes == null || !notes.Any())
            {
                return new Notes().Date.GetHashCode();
            }
            var foundedNote = FindNote(notes);
            return ReviewNote(foundedNote) ? foundedNote.Date.GetHashCode() : new Notes().Date.GetHashCode();
        }

        public virtual bool ReviewNote(Notes note)
        {
            if (note == null)
            {
                Console.WriteLine("Note doesn't found");
                return false;
            }
            Console.WriteLine("Founded Note: ");
            CheckNote(note);
            Console.Write("You want to work with this Note [Yes/No]?" +
                          "\nEnter [0/1]: ");
            try
            {
                int number = Int32.Parse(_consoleInput.ReadLine());
                bool result = (number == 0);
                return result;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"FormatException: {ex.Message}");
                log.Error(ex.Message);
                return false;
            }
            catch (OverflowException ex)
            {
                Console.WriteLine($"OverflowExcpetion: {ex.Message}");
                log.Error(ex.Message);
                return false; 
            }
        }
        public virtual Notes FindNote(Dictionary<int, Notes> notes)
        {
            Console.WriteLine("Input a pattern: ");
            var query = _consoleInput.ReadLine();
            if (notes == null || !notes.Any() || string.IsNullOrEmpty(query))
            {
                Console.WriteLine("Dictionary or patter is empty");
                return null;
            }
            Notes bestMatchingNote = null;
            var bestDistance = int.MaxValue; 
            var queryLength = query.Length;

            foreach (var note in notes)
            {
                var distance = FuzzySearch.CalculateLevenshteinDistance(query, note.Value.Title);
                if (distance < bestDistance || (distance == bestDistance && note.Value.Title.Length < queryLength))
                {
                    bestMatchingNote = note.Value;
                    bestDistance = distance;
                }
            }

            return bestMatchingNote;

        }
        public void CheckNote(Notes note)
        {
            if (note == null)
            {
                Console.WriteLine("No note found matching the substring!");
                return;
            }
            var chunkSize = 35;
            List<string> chunks = new List<string>();
            for (int i = 0; i < note.Description.Length; i += chunkSize)
            {
                var length = Math.Min(chunkSize, note.Description.Length - i);
                var chunk = note.Description.Substring(i, length);
                chunks.Add(chunk);
            }
            Console.WriteLine("Title           || Description                          || Date");
            Console.WriteLine("------------------------------------------------------------------------------");
            var maxChunks = chunks.Count;
            for (int i = 0; i < maxChunks; i++)
            {
                var title = i == 0 ? note.Title : string.Empty;
                var description = i < chunks.Count ? chunks[i] : string.Empty;
                var date = i == 0 ? note.Date.ToString("dd/MM/yyyy HH:mm:ss") : string.Empty;
                Console.WriteLine($"{title.PadRight(_maxTitleLenght)} || {description.PadRight(_maxDescrLenght)}  || {date}");
            }
            Console.WriteLine("------------------------------------------------------------------------------");
        }
        public void CheckNotes(Dictionary<int, Notes> notes)
        {
            if(notes == null || !notes.Any())
            {
                Console.WriteLine("You have no notes!");
                return;
            }
            Console.WriteLine("Title           || Description                          || Date");
            Console.WriteLine("------------------------------------------------------------------------------");
            foreach (var kvp in notes.OrderBy(n => n.Value.Date))
            {
                var title = kvp.Value.Title;
                var descr = kvp.Value.Description;
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
