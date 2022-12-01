using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalcBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            Console.WriteLine($"Got message for {GetType().Name} controller");
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id, $"Got button pressed message", cancellationToken: ct);
        }
    }
}
