using CalcBot.Services;
using CalcBot.Utilities;
using CalcBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace CalcBot.Controllers
{
    public class InlineKeyboardController
    {
        private readonly ILogger _logger;
        private readonly IStorage _memStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, ILogger logger, IStorage memstorage)
        {
            _telegramClient = telegramBotClient;
            _logger = logger;
            _memStorage = memstorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            Session? sess = _memStorage.GetSession(callbackQuery.From.Id);
            if (sess == null)
            {
                sess = _memStorage.UpsertSession(callbackQuery.From.Id, OperationType.None);
            }

            OperationType operationSelected = callbackQuery.Data switch
            {
                OperationTypes.CalcSum => OperationType.CaclSum,
                OperationTypes.SymbolCount => OperationType.SymbolCount,
                _ => OperationType.None
            };

            _memStorage.UpsertSession(callbackQuery.From.Id, operationSelected);

            string operationSelectedText = callbackQuery.Data switch
            {
                OperationTypes.CalcSum => "calculating sum of numbers for new messages",
                OperationTypes.SymbolCount => "counting symbol count for new messages",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Now {operationSelectedText}.</b>{Environment.NewLine}{Environment.NewLine}" +
                $"You can switch operation in main menu", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
