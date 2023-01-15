using System.Linq;
using System.Threading.Tasks;
using MemesFinderGreeter.Extensions;
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
        public async Task Run([ServiceBusTrigger("newmembersmessages", "greeter", Connection = "ServiceBusOptions")]Update tgIncomeMessage)
        {
            var newMembers = _chatMemberManager.GetNewChatMember(tgIncomeMessage);
            if (!newMembers.Any())
                return;

            var currentChat = tgIncomeMessage.GetChat();
            var chatAdminsUsernames = await _chatMemberManager.GetChatAdminsUsernames(currentChat.Id);
            var currentChatOptions = _options.ChatOptions.FirstOrDefault(options => options.ChatId == currentChat.Id);

            if (currentChatOptions is null)
            {
                _logger.LogError($"No options set for chat with Id: {currentChat.Id}");
                return;
            }

            foreach (var member in newMembers)
            {
                var formattedGreeting = _greetingsFormatter
                    .FormatGreetingMessage(currentChatOptions.GreetingsMarkdownTemplate, member, chatAdminsUsernames);

                await _telegramBotClient.SendTextMessageAsync(
                    chatId: member.ChatId,
                    messageThreadId: currentChatOptions.GreetingsThreadId,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.MarkdownV2,
                    text: formattedGreeting);
            }
        }
    }
}

