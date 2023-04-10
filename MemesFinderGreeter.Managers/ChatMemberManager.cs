﻿using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Models;
using MemesFinderGreeter.Models.Options;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemesFinderGreeter.Managers;

public class ChatMemberManager : IChatMemberManager
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly ILogger<ChatMemberManager> _logger;

    public ChatMemberManager(ITelegramBotClient telegramBotClient, ILogger<ChatMemberManager> logger)
    {
        _telegramBotClient = telegramBotClient;
        _logger = logger;
    }

    public async Task<IEnumerable<string>> GetChatAdminsUsernames(long chatId)
    {
        var result = new List<string>();
        try
        {
            var chatAdmins = await _telegramBotClient.GetChatAdministratorsAsync(chatId);
            result = chatAdmins.Select(admin => admin.User.Username ?? admin.User.FirstName)
                                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting chat admins", ex);
        }
        return result;
    }

    public IEnumerable<GreetingMemberSettings> GetNewChatMember(Update tgUpdate, ChatOptions chatOptions)
    {
        if (tgUpdate?.Message?.NewChatMembers is null)
            yield break;

        foreach (var member in tgUpdate.Message.NewChatMembers)
        {
            yield return new GreetingMemberSettings
            {
                ChatId = tgUpdate.Message.Chat.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Username = member.Username,
                MemberId = member.Id,
                RulesLink = chatOptions.GreetingsRulesLink
            };
        }

    }
}

