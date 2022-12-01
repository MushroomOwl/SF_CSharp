using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalcBot.Controllers
{
    public class DefaultMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Got message for {GetType().Name} controller");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Message type isn't supported", cancellationToken: ct);
        }
    }
}
