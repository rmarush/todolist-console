using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using todolist_console.Models;

namespace todolist_console.Services
{
    public class JsonService
    {
        private static readonly string _DataFolderPath = @"..\..\..\Data\";
        public static Dictionary<int, Notes> LoadNoteData()
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(_DataFolderPath, "NoteData.json"));
                var notes = JsonSerializer.Deserialize<Dictionary<int, Notes>>(json);
                return notes;
            }
            catch(JsonException ex)
            {
                Console.WriteLine($"Deserialization error NoteData.json: {ex.Message}");
                return new Dictionary<int, Notes>();
            }
            catch(IOException ex)
            {
                Console.WriteLine($"Error reading NoteData.json: {ex.Message}");
                return new Dictionary<int, Notes>();
            }
            
        }
        public static void WriteNoteData(Dictionary<int, Notes> notes)
        {
            try
            {
                string json = JsonSerializer.Serialize(notes);
                File.WriteAllText(Path.Combine(_DataFolderPath, "NoteData.json"), json);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error NoteData.json: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading NoteData.json: {ex.Message}");
            }

        }
        public static List<Tasks> LoadTaskData()
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(_DataFolderPath, "TaskData.json"));
                var tasks = JsonSerializer.Deserialize<List<Tasks>>(json);
                return tasks;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error TaskData.json: {ex.Message}");
                return new List<Tasks>();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading TaskData.json: {ex.Message}");
                return new List<Tasks>();
            }

        }
        public static void WriteTaskData(List<Tasks> tasks)
        {
            try
            {
                string json = JsonSerializer.Serialize(tasks);
                File.WriteAllText(Path.Combine(_DataFolderPath, "TaskData.json"), json);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error TaskData.json: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading TaskData.json: {ex.Message}");
            }
        }
    }
}
