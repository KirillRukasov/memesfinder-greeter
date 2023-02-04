namespace MemesFinderGreeter.Options
{
    public class ChatOptions
    {
        public long? ChatId { get; set; }
        public string GreetingsMarkdownTemplate { get; set; }
        public int? GreetingsThreadId { get; set; }
        public string GreetingsRulesLink { get; set; }
        public string GreetingsSubstituteTextLink { get; set; }
    }
}

