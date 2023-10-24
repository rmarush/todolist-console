using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using todolist_console.Enums;

namespace todolist_console.Models
{
    public class Tasks
    {
        private string title;
        private TasksStatus status;
        private DateTime date;
        public string Title { get { return title; } set { title = value; } }
        public TasksStatus Status { get { return status; } set { status = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public Tasks(string title, TasksStatus status)
        {
            this.title = title;
            this.status = status;
            this.date = DateTime.Now;
        }
        public override string ToString()
        {
            return string.Format($"{title}\n" +
                                 $"{status.ToString()}\n" +
                                 $"{date:dd/MM/yyyy HH:mm:ss}");
        }
    }
}
