using CalcBot.Utilities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CalcBot.Controllers
{
    public class DefaultMessageController
    {
        private readonly ILogger _logger;
        private readonly ITelegramBotClient _telegramClient;

        public DefaultMessageController(ITelegramBotClient telegramBotClient, ILogger logger)
        {
            _telegramClient = telegramBotClient;
            _logger = logger;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            _logger.Event($"Got message for {GetType().Name} controller");
            await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"Message type isn't supported", cancellationToken: ct);
        }
    }
}
