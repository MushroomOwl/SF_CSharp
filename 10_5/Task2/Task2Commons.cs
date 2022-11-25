namespace Task2Commons
{
    interface ILogger
    {
        void Event(string message);
        void Error(string message);
    }

    class Logger : ILogger
    {
        static ConsoleColor ErrorLogColor = ConsoleColor.Red;
        static ConsoleColor EventLogColor = ConsoleColor.Blue;

        public void Event(string message)
        {
            Console.ForegroundColor = EventLogColor;
            Console.WriteLine("LOG: " + message);
            Console.ResetColor();
        }

        public void Error(string message)
        {
            Console.ForegroundColor = ErrorLogColor;
            Console.WriteLine("ERROR: " + message);
            Console.ResetColor();
        }
    }

    interface IConsoleReader
    {
        void Read(out int val, ILogger? logger, string? variableName = null);
    }

    class ConsoleReader : IConsoleReader
    {
        public void Read(out int value, ILogger? logger, string? variableName = null)
        {
            bool firstTry = true;
            while (true)
            {
                try
                {
                    string text = firstTry ?
                        string.Format("Input int value {0}: ", variableName ?? "") :
                        string.Format("Please input correct integer value {0}: ", variableName ?? "");
                    Console.Write(text);
                    string? input = Console.ReadLine();
                    value = Convert.ToInt32(input);
                    return;
                }
                catch (Exception ex)
                {
                    if (logger != null) logger.Error(String.Format("Input exception - {0}: {1}", ex.GetType(), ex.Message));
                    firstTry = false;
                }
            }
        }
    }
}