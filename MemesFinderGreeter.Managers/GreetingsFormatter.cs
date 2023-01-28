using System;
using System.Text;
using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Managers.Extensions;

namespace MemesFinderGreeter.Managers
{
	public class GreetingsFormatter : IGreetingsFormatter
    {
        public string FormatGreetingMessage<T>(string template, T chatMember, IEnumerable<string> adminUsernames)
        {
            var greetingsBuilder = new StringBuilder(template);
            var modelFields = typeof(T).GetProperties();

            foreach(var field in modelFields)
                greetingsBuilder.Replace($"{{{field.Name}}}", field?.GetValue(chatMember)?.ToString());

            greetingsBuilder.AppendLine($"\n\n{adminUsernames.GetFormattedString(admin => $"@{admin}")}");

            return greetingsBuilder.ToString();
        }
    }
}

