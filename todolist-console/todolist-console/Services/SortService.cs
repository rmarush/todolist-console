using System.Diagnostics;
using todolist_console.Enums;
using todolist_console.Utils;

namespace todolist_console.Services
{
    public class SortService
    {
        public static void SortCollectionByField<T>(ICollection<T> collection) where T : class
        {
            var sortedList = collection.ToList();

            Console.Write("Input a field number: ");
            Console.WriteLine("\n1 - Sort by Title" +
                              "\n2 - Sort by Date" +
                              "\n3 - End");
            Console.Write("Input a choice: ");
            Enum.TryParse(Console.ReadLine(), out SortMenu fieldIndex);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            switch (fieldIndex)
            {
                case SortMenu.SortByTitle:
                    Sort.QuickSort(sortedList, x => (string)typeof(T).GetProperty("Title").GetValue(x));
                    break;

                case SortMenu.SortByDate:
                    Sort.QuickSort(sortedList, (Func<T, DateTime>)(x => (DateTime)typeof(T).GetProperty("Date").GetValue(x)));
                    break;

                case SortMenu.End:
                    Console.WriteLine("Exit to menu");
                    return;

                default:
                    throw new ArgumentException("Invalid field index for sorting");
            }
            stopwatch.Stop();
            collection.Clear();
            foreach (var item in sortedList)
            {
                collection.Add(item);
            }

            Console.WriteLine($"Collection was sorted in {stopwatch.Elapsed} milliseconds!");
        }

    }
}
