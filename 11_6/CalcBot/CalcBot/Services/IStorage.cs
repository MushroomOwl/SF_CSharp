using CalcBot.Models;

namespace CalcBot.Services
{
    public interface IStorage
    {
        Session? GetSession(long chatId);
        Session UpsertSession(long chatId, OperationType operation);
    }
}
