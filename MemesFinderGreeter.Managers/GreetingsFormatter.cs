using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Managers.Extensions;
using MemesFinderGreeter.Models;
using System.Text;

namespace MemesFinderGreeter.Managers
{
    public class GreetingsFormatter : IGreetingsFormatter
    {
        public string FormatGreetingMessage<T>(string template, T chatMember, IEnumerable<string> adminUsernames, GreetingTextField greetingTextField)
        {
            var greetingsBuilder = new StringBuilder(template);
            var chatMemberFields = typeof(T).GetProperties();
            var greetingTextFields = typeof(GreetingTextField).GetProperties();

            foreach (var field in chatMemberFields)
                greetingsBuilder.Replace($"{{{field.Name}}}", field?.GetValue(chatMember)?.ToString());

            foreach (var field in greetingTextFields)
                greetingsBuilder.Replace($"{{{field.Name}}}", field?.GetValue(greetingTextField)?.ToString());

            greetingsBuilder.AppendLine($"\n\n{adminUsernames.GetFormattedString(admin => $"@{admin}")}");

            return greetingsBuilder.ToString();
        }
    }
}

