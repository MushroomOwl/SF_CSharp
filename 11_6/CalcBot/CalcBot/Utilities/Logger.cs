using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CalcBot.Utilities
{
    class Logger : ILogger
    {
        static ConsoleColor ErrorLogColor = ConsoleColor.Red;
        static ConsoleColor EventLogColor = ConsoleColor.Green;
        static ConsoleColor WarnLogColor = ConsoleColor.Yellow;

        private string nowAsString()
        {
            return DateTime.Now.ToUniversalTime().ToString("o");
        }

        public void Event(string message)
        {
            Console.ForegroundColor = EventLogColor;
            Console.WriteLine("[{0}] LOG: {1}", nowAsString(), message);
            Console.ResetColor();
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = WarnLogColor;
            Console.WriteLine("[{0}] WARN: {1}", nowAsString(), message);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ErrorLogColor;
            Console.WriteLine("[{0}] ERROR: {1}", nowAsString(), message);
            Console.ResetColor();
        }

        public void Error(Exception ex, string message)
        {
            string output = string.Format("[{0}] ERROR: {1}\n" +
                "Exception: {2} - {3}\n" +
                "StackTrace: {4}", nowAsString(), message, ex.GetType(), ex.Message, ex.StackTrace);

            Error(output);
        }

        public void Error(Exception ex)
        {
            Error(ex, "");
        }
    }
}
