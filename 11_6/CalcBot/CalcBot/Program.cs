using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using CaclBot;
using CalcBot.Controllers;
using CalcBot.Services;
using CalcBot.Config;
using CalcBot.Utilities;

namespace CalcBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;
            ILogger logger = new Logger();

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();

            logger.Event("Service started. Listening...");
            await host.RunAsync();
            logger.Event("Service shut down.");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            ILogger logger = new Logger();
            Settings appSettings = Settings.InitFromFile(logger);

            services.AddSingleton(appSettings);
            services.AddSingleton(logger);
            services.AddSingleton<IStorage, MemStorage>();
            services.AddSingleton<ITextMessageProcessor, TextMessageProcessor>();

            services.AddTransient<DefaultMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();

            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5885786714:AAGXywUQLP5RxhPNs1nsMNGqj0EGzGp-9xQ"));
            services.AddHostedService<Bot>();
        }
    }
}