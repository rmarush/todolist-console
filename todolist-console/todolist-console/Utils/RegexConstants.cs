
namespace todolist_console.Utils
{
    public class RegexConstants
    {
        public const string TitlePattern = @"^[\p{L}]+[\p{L}0-9\s]*$";
        public const string EmailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
    }
}
