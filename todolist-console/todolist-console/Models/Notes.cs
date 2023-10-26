using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist_console.Models
{
    public class Notes
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public Notes()
        {

        }
        public Notes(string title, string description)
        {
            Title = title;
            Description = description;
            Date = DateTime.Now;
        }
    }
}
