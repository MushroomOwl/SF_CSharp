using CalcBot.Models;
using CalcBot.Services;
using CalcBot.Utilities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace CalcBot.Controllers
{
    public class TextMessageController
    {
        private readonly ILogger _logger;
        private readonly IStorage _memStorage;
        private readonly ITelegramBotClient _telegramClient;
        private readonly ITextMessageProcessor _textMessageProcessor;


        public TextMessageController(ITelegramBotClient telegramBotClient, ILogger logger, ITextMessageProcessor textMessageProcessor, IStorage memstorage)
        {
            _telegramClient = telegramBotClient;
            _logger = logger;
            _textMessageProcessor = textMessageProcessor;
            _memStorage = memstorage;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($"Sum" , OperationTypes.CalcSum),
                        InlineKeyboardButton.WithCallbackData($"Text length" , OperationTypes.SymbolCount)
                    });

                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"This bot supports operations: {Environment.NewLine}{Environment.NewLine}" +
                        $" \u00B7 Calculate sum of numbers in message{Environment.NewLine}" +
                        $" \u00B7 Calculate message text length{Environment.NewLine}{Environment.NewLine}" +
                        $"Please select operation to preform", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    Session? session = _memStorage.GetSession(message.Chat.Id);

                    if (session == null || session.CurrentOperation == OperationType.None)
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Please select operation via menu or `/start` command", cancellationToken: ct);
                    }
                    else
                    {
                        try
                        {
                            TextProcessorResponse res = _textMessageProcessor.Do(message.Text, session.CurrentOperation);

                            if (res.Ok)
                            {
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, res.Text, cancellationToken: ct);
                            }
                            else
                            {
                                await _telegramClient.SendTextMessageAsync(message.Chat.Id, res.ErrorMessage, cancellationToken: ct);
                                _logger.Warn(String.Format("User ecountered an error (chatId = {0}):\n" +
                                    "INPUT: {1}\n" +
                                    "OUTPUT: {2}", message.Chat.Id, message.Text, res.ErrorMessage
                               ));
                            }
                        }
                        catch (Exception ex) {
                            _logger.Error(ex, String.Format("User ecountered an unexpected error (chatId = {0}):\n" +
                                    "INPUT: {1}", message.Chat.Id, message.Text
                            ));
                            await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Unfortunately, unexpected error occured, please try again later.", cancellationToken: ct);
                        }
                    }

                    break;
            }
        }
    }
}
