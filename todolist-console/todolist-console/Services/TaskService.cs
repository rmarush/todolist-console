using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using todolist_console.Models;
using todolist_console.Enums;
using System.Threading.Tasks;

namespace todolist_console.Services
{
    internal class TaskService
    {
        public Tasks CreateTask()
        {
            Tasks newTask = null;
            while (newTask == null)
            {
                Console.Write("Enter a task name => ");
                string title = Console.ReadLine();

                if (!string.IsNullOrEmpty(title) && Regex.IsMatch(title, "^[A-Za-z]+[0-9]*$"))
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
            TasksStatus status = new TasksStatus();
            bool exit = false;
            while(!exit)
            {
                Console.WriteLine("Tasks status:" +
                      "\n1 - To-Do" +
                      "\n2 - In progress" +
                      "\n3 - Done");
                Console.Write("Input a choice: ");
                Enum.TryParse(Console.ReadLine(), out status);
                switch(status)
                {
                    case TasksStatus.ToDo:
                    case TasksStatus.InProgress:
                    case TasksStatus.Done:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }
            task.Status = status;
            Console.WriteLine("Status was changed!");
        }
        public Tasks FindTask(List<Tasks> tasks)
        {
            Tasks task = null;
            while (task == null)
            {
                Console.Write("Enter a task name for find => ");
                string foundedTitle = Console.ReadLine();
                Tasks foundedTask = tasks.FirstOrDefault(t => t.Title == foundedTitle);
                if (foundedTask != null)
                {
                    task = foundedTask;
                }
                else
                {
                    Console.WriteLine("Task not found. Please try again.");
                }
            }
            return task;
        }
        public void CheckTasks(List<Tasks> tasks)
        {
            IEnumerable<TasksStatus> uniqueStatuses = tasks.Select(task => task.Status).Distinct().OrderBy(status => status);
            Console.WriteLine("Status         || Title                          || Date");
            Console.WriteLine("-----------------------------------------------------------------------");
            foreach (TasksStatus status in uniqueStatuses)
            {
                Console.WriteLine(status.ToString().PadRight(15) + "||" + $"  {"".PadRight(30)}||");
                IEnumerable<Tasks> tasksWithStatus = tasks.Where(task => task.Status == status);
                foreach (Tasks task in tasksWithStatus)
                {
                    Console.WriteLine("               || " + task.Title.PadRight(30) + " || " + task.Date.ToString("dd/MM/yyyy HH:mm:ss"));
                }
                Console.WriteLine("-----------------------------------------------------------------------");
            }
        }
    }
}