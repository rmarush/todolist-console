using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist_console.Models
{
    public class MailKey
    {
        public string Key {get; set; }
        public string Value { get; set; }
        public MailKey() { }
        public MailKey(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
