using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist_console.Models
{
    public class Notes
    {
        private string title;
        private string description;
        private DateTime date;
        public string Title { get { return title; } set { title = value; } }
        public string Description { get { return description; } set { description = value; } }
        public DateTime Date { get { return date; } set { date = value; } }
        public Notes(string title, string description)
        {
            this.title = title;
            this.description = description;
            this.date = DateTime.Now;
        }
        public override string ToString()
        {
            return string.Format($"{title}\n" +
                                 $"{description}\n" +
                                 $"{date:dd/MM/yyyy HH:mm:ss}");
        }
    }
}
