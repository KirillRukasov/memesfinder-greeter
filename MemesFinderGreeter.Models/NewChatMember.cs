namespace MemesFinderGreeter.Models;

public class NewChatMember
{
    public long MemberId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public long ChatId { get; set; }
}

