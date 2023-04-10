using System;
using MemesFinderGreeter.Interfaces;
using MemesFinderGreeter.Managers;
using MemesFinderGreeter.Models.Options;
using MemesFinderGreeter.Options;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;

[assembly: FunctionsStartup(typeof(MemesFinderGreeter.Startup))]
namespace MemesFinderGreeter
{
	public class Startup : FunctionsStartup
	{
        private IConfigurationRoot _functionConfig;

        public override void Configure(IFunctionsHostBuilder builder)
        {
            _functionConfig = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            builder.Services.Configure<TelegramBotOptions>(_functionConfig.GetSection("TelegramBotOptions"));
            builder.Services.Configure<GreeterOptions>(_functionConfig.GetSection("GreeterOptions"));

            builder.Services.AddSingleton<ITelegramBotClient>(factory =>
                new TelegramBotClient(factory.GetRequiredService<IOptions<TelegramBotOptions>>().Value.Token));

            builder.Services.AddTransient<IChatMemberManager, ChatMemberManager>();
            builder.Services.AddTransient<IGreetingsFormatter, GreetingsFormatter>();

            builder.Services.AddLogging();
        }
    }
}

