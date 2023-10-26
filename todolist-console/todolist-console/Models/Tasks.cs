using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todolist_console.Enums;

namespace todolist_console.Models
{
    public class Tasks
    {
        public string Title { get; set; }
        public TasksStatus Status { get; set; }
        public DateTime Date { get; set; }
        public Tasks()
        {

        }
        public Tasks(string title, TasksStatus status)
        {
            Title = title;
            Status = status;
            Date = DateTime.Now;
        }
    }
}
