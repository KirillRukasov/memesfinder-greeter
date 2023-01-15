using MemesFinderGreeter.Models;
using Telegram.Bot.Types;

namespace MemesFinderGreeter.Interfaces;

public interface IChatMemberManager
{
    public IEnumerable<NewChatMember> GetNewChatMember(Update tgUpdate);
    public Task<IEnumerable<string?>> GetChatAdminsUsernames(long chatId);
}

