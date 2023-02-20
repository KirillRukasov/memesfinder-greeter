using MemesFinderGreeter.Models;
using Telegram.Bot.Types;

namespace MemesFinderGreeter.Interfaces;

public interface IChatMemberManager
{
    public IEnumerable<GreetingMemberSettings> GetNewChatMember(Update tgUpdate, string rulesLink);
    public Task<IEnumerable<string>> GetChatAdminsUsernames(long chatId);
}

