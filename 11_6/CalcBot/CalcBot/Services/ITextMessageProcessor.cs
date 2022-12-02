using CalcBot.Models;

namespace CalcBot.Services
{
    public interface ITextMessageProcessor {
        TextProcessorResponse Do(string message, OperationType type);
    }
}
