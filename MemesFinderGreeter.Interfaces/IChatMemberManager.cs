using MemesFinderGreeter.Models;
using MemesFinderGreeter.Models.Options;
using Telegram.Bot.Types;

namespace MemesFinderGreeter.Interfaces;

public interface IChatMemberManager
{
    public IEnumerable<GreetingMemberSettings> GetNewChatMember(Update tgUpdate, ChatOptions chatOptions);
    public Task<IEnumerable<string>> GetChatAdminsUsernames(long chatId);
}

