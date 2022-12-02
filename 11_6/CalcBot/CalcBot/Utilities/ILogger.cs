namespace CalcBot.Utilities
{
    public interface ILogger
    {
        void Event(string message);
        void Warn(string message);
        void Error(string message);
        void Error(Exception ex, string message);
        void Error(Exception ex);
    }
}
