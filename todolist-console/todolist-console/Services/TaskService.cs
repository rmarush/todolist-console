using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using todolist_console.Models;
using todolist_console.Enums;
using todolist_console.Utils;

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
            bool exit = false;
            while(!exit)
            {
                Console.WriteLine("Tasks status:" +
                      "\n1 - To-Do" +
                      "\n2 - In progress" +
                      "\n3 - Done");
                Console.Write("Input a choice: ");
                Enum.TryParse(Console.ReadLine(), out TasksStatus status);
                switch(status)
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
        public Tasks FindTask(List<Tasks> tasks)
        {
            Console.Write("Enter a task name for find => ");
            string foundedTitle = Console.ReadLine();
            var task = tasks.FirstOrDefault(t => t.Title == foundedTitle);    
            return task ?? new Tasks();
        }
        public void CheckTasks(List<Tasks> tasks)
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
    }
}