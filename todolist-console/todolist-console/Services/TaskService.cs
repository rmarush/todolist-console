using System.Text.RegularExpressions;
using todolist_console.Models;
using todolist_console.Enums;
using todolist_console.Utils;
using todolist_console.Utils.Interfaces;

namespace todolist_console.Services
{
    public class TaskService
    {
        private readonly IConsoleInput _consoleInput;

        public TaskService() { }
        public TaskService(IConsoleInput consoleInput)
        {
            _consoleInput = consoleInput;
        }
        public Tasks CreateTask()
        {
            Tasks newTask = null;
            while (newTask == null)
            {
                Console.Write("Enter a task name => ");
                var title = _consoleInput.ReadLine();

                if (!string.IsNullOrEmpty(title) && Regex.IsMatch(title, RegexConstants.TitlePattern))
                {
                    newTask = new Tasks(title, TasksStatus.ToDo);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }

            Console.WriteLine("Task was created!");
            return newTask;
        }

        public void EditTask(Tasks task)
        {
            var exit = false;
            while (!exit)
            {
                Console.WriteLine("Tasks status:" +
                                  "\n1 - To-Do" +
                                  "\n2 - In progress" +
                                  "\n3 - Done");
                Console.Write("Input a choice: ");
                Enum.TryParse(_consoleInput.ReadLine(), out TasksStatus status);
                switch (status)
                {
                    case TasksStatus.ToDo:
                    case TasksStatus.InProgress:
                    case TasksStatus.Done:
                        task.Status = status;
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please try again");
                        break;
                }
            }

            Console.WriteLine("Status was changed!");
        }

        public void CheckTaskByOne(DoublyLinkedList<Tasks> tasks)
        {
            var current = tasks.Head;
            ConsoleKeyInfo key;
            do
            {
                if (tasks.Count == 0) return;
                Console.WriteLine("Status         || Title                          || Date");
                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine(current.Value.Status.ToString().PadRight(15) + "||" + $"  {"".PadRight(30)}||");
                Console.WriteLine("               || " + current.Value.Title.PadRight(30) + " || " + current.Value.Date.ToString("dd/MM/yyyy HH:mm:ss"));
                Console.WriteLine("-----------------------------------------------------------------------");
                CheckKey();
                key = Console.ReadKey();
                ProcessKeyPress(key, tasks, ref current);
                Console.Clear();
            } while (key.Key != ConsoleKey.Escape);
        }

        private void CheckKey()
        {
            Console.WriteLine("\t\t╔════════════════════════════════╗");
            Console.WriteLine("\t\t║  ↑ (Up Arrow) - Edit           ║");
            Console.WriteLine("\t\t║  <-- (Left Arrow) - Previous   ║");
            Console.WriteLine("\t\t║  --> (Right Arrow) - Next      ║");
            Console.WriteLine("\t\t║  ↓ (Down Arrow) - Delete       ║");
            Console.WriteLine("\t\t║  Esc (Escape) - Exit           ║");
            Console.WriteLine("\t\t╚════════════════════════════════╝");
        }
        private void ProcessKeyPress(ConsoleKeyInfo key, DoublyLinkedList<Tasks> tasks, ref DoublyLinkedList<Tasks>.Node current)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    current = current.Previous;
                    return;
                case ConsoleKey.RightArrow:
                    current = current.Next;
                    return;
                case ConsoleKey.UpArrow:
                    EditTask(current.Value);
                    break;
                case ConsoleKey.DownArrow:
                    var deleteNode = current;
                    current = current.Next;
                    tasks.Remove(deleteNode.Value);
                    break;
                case ConsoleKey.Escape:
                default:
                    break;
            }
            
        }

        public void CheckTasks(DoublyLinkedList<Tasks> tasks)
        {
            IEnumerable<TasksStatus> uniqueStatuses = tasks.Select(task => task.Status).Distinct().OrderBy(status => status);
            if(uniqueStatuses == null ||  !uniqueStatuses.Any())
            {
                Console.WriteLine("The task list is empty!");
                return;
            }
            Console.WriteLine("Status         || Title                          || Date");
            Console.WriteLine("-----------------------------------------------------------------------");
            foreach (var status in uniqueStatuses)
            {
                Console.WriteLine(status.ToString().PadRight(15) + "||" + $"  {"".PadRight(30)}||");
                var tasksWithStatus = tasks.Where(task => task.Status == status);
                foreach (var task in tasksWithStatus)
                {
                    Console.WriteLine("               || " + task.Title.PadRight(30) + " || " + task.Date.ToString("dd/MM/yyyy HH:mm:ss"));
                }
                Console.WriteLine("-----------------------------------------------------------------------");
            }
        }
        public async Task EmailSend(DoublyLinkedList<Tasks> tasks)
        {
            var filePath = "Excel-To-Do-List.xlsx";
            Console.WriteLine("Input reciver email: ");
            var email = Console.ReadLine();
            if (!string.IsNullOrEmpty(email) && Regex.IsMatch(email, RegexConstants.EmailPattern))
            {
                await ExcelService.CreateExcelTable(tasks, filePath);
                await MailService.SendMessage(email.Trim(), filePath);

            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
                return;
            }

        }
    }
}