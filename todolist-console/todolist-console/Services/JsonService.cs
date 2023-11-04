using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using todolist_console.Models;
using todolist_console.Utils;

namespace todolist_console.Services
{
    public class JsonService
    {
        private static readonly string _DataFolderPath = @"..\..\..\Data\";

        public static T? LoadData<T>(string path) where T : class
        {
            try
            {
                string json = File.ReadAllText(Path.Combine(_DataFolderPath, path));
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error {path}: {ex.Message}");
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading {path}: {ex.Message}");
                return null;
            }
        }
        public static void WriteData<T>(T value, string path) where T : class
        {
            try
            {
                string json = JsonSerializer.Serialize(value);
                File.WriteAllText(Path.Combine(_DataFolderPath, path), json);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error {path}: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error reading {path}: {ex.Message}");
            }
        }
    }
}
