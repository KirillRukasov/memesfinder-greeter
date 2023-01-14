using System.Threading.Tasks;
using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Options;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MemesFinderGreeter
{
    public class Greetings
    {
        private readonly ILogger<Greetings> _logger;
        private readonly IChatMemberManager _chatMemberManager;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IGreetingsFormatter _greetingsFormatter;
        private readonly GreeterOptions _options;

        public Greetings(
            ILogger<Greetings> log,
            IChatMemberManager chatMemberManager,
            ITelegramBotClient telegramBotClient,
            IOptions<GreeterOptions> options,
            IGreetingsFormatter greetingsFormatter)
        {
            _logger = log;
            _chatMemberManager = chatMemberManager;
            _telegramBotClient = telegramBotClient;
            _greetingsFormatter = greetingsFormatter;
            _options = options.Value;
        }

        [FunctionName("Greetings")]
        public async Task Run([ServiceBusTrigger("allmessages", "greeter", Connection = "ServiceBusOptions")]Update tgIncomeMessage)
        {
            var newMembers = _chatMemberManager.GetNewChatMember(tgIncomeMessage);
            foreach (var member in newMembers)
                await _telegramBotClient.SendTextMessageAsync(
                    chatId: member.ChatId,
                    messageThreadId: _options.GreetingsThreadId,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2,
                    text: _greetingsFormatter.FormatGreetingMessage(_options.GreetingsMarkdownTemplate, member)
                );
        }
    }
}

