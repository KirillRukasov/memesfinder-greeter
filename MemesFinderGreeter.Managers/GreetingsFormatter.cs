using System;
using System.Text;
using MemesFinderGreeter.Interfaces;

namespace MemesFinderGreeter.Managers
{
	public class GreetingsFormatter : IGreetingsFormatter
    {
        public string FormatGreetingMessage<T>(string template, T chatMember)
        {
            var greetingsBuilder = new StringBuilder(template);
            var modelFields = typeof(T).GetProperties();
            foreach(var field in modelFields)
                greetingsBuilder.Replace($"{{{field.Name}}}", field?.GetValue(chatMember)?.ToString());
            return greetingsBuilder.ToString();
        }
    }
}

