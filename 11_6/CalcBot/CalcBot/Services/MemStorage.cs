using CalcBot.Models;
using System.Collections.Concurrent;

namespace CalcBot.Services
{
    public class MemStorage : IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        public Session? GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
            {
                return _sessions[chatId];
            }

            return null;
        }

        public Session UpsertSession(long chatId, OperationType operation)
        {
            if (_sessions.ContainsKey(chatId))
            {
                _sessions[chatId].CurrentOperation = operation;
                return _sessions[chatId];
            }

            var newSession = new Session(operation);
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
