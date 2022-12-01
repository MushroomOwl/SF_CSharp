using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalcBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Got message for {GetType().Name} controller");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Received text message", cancellationToken: ct);
        }
    }
}
