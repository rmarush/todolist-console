using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolist_console.Utils
{
    public class RegexConstants
    {
        public const string TitlePattern = @"^[\u0400-\u052F\u2DE0-\u2DFF\uA640-\uA69Fa-zA-Z\s]+$";
        public const string EmailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
    }
}
