using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist_console.Models
{
    internal class Task
    {
        private string title;
        private TaskStatus status;
        private DateTime date;
        public string Title { get { return title; } set { title = value; } }
        public TaskStatus Status { get { return status; } set { status = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public Task(string title, TaskStatus status)
        {
            this.title = title;
            this.status = status;
            this.date = DateTime.Now;
        }
        public override string ToString()
        {
            return string.Format($"{title}\n" +
                                 $"{date:dd/MM/yyyy HH:mm:ss}");
        }
    }
}
