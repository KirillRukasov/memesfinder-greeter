using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Models;
using Telegram.Bot.Types;

namespace MemesFinderGreeter.Managers;

public class ChatMemberManager : IChatMemberManager
{
    public IEnumerable<NewChatMember> GetNewChatMember(Update tgUpdate)
    {
        if (tgUpdate?.Message?.NewChatMembers is null)
            yield break;

        foreach(var member in tgUpdate.Message.NewChatMembers)
        {
            yield return new NewChatMember
            {
                ChatId = tgUpdate.Message.Chat.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Username = member.Username,
                MemberId = member.Id
            };
        }

    }
}

