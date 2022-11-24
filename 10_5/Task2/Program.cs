class Program
{
    interface ISummarizer<T>
    {
        T Sum(T x, T y, ILogger? logger);
    }

    interface ISubtractor<T>
    {
        T Subtract(T x, T y, ILogger? logger);
    }

    interface IMultiplier<T>
    {
        T Multiply(T x, T y, ILogger? logger);
    }

    interface IIntDivider<T>
    {
        T Divide(T x, T y, ILogger? logger);
    }

    class Calcint : ISummarizer<int>, ISubtractor<int>, IMultiplier<int>, IIntDivider<int>
    {
        int ISummarizer<int>.Sum(int x, int y, ILogger? logger)
        {
            int result = x + y;
            if (logger != null) logger.Event(string.Format("x + y = {0}", result));
            return result;
        }

        int ISubtractor<int>.Subtract(int x, int y, ILogger? logger)
        {
            int result = x - y;
            if (logger != null) logger.Event(string.Format("x - y = {0}", result));
            return result;
        }

        int IMultiplier<int>.Multiply(int x, int y, ILogger? logger)
        {
            int result = x * y;
            if (logger != null) logger.Event(string.Format("x * y = {0}", result));
            return result;
        }

        int IIntDivider<int>.Divide(int x, int y, ILogger? logger)
        {
            try
            {
                int result = x / 0;
                if (logger != null) logger.Event(string.Format("x / y = {0}", result));
                return result;
            }
            catch (Exception ex) {
                if (logger != null) logger.Error(ex.GetType() + " - " + ex.Message);
                return 0;
            }
        }
    }

    interface IConsoleReader
    {
        int Read();
    }

    interface ILogger
    {
        void Event(string message);
        void Error(string message);
    }

    class ConsoleProcessor : IConsoleReader, ILogger
    {
        static ConsoleColor ErrorLogColor = ConsoleColor.Red;
        static ConsoleColor EventLogColor = ConsoleColor.Blue;

        public int Read()
        {
            bool firstTry = true;
            while (true)
            {
                try
                {
                    Console.Write(firstTry ? "Input int value: " : "Please input correct integer value: ");
                    string? input = Console.ReadLine();
                    return Convert.ToInt32(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Input exception - {0}: {1}", ex.GetType(), ex.Message);
                    firstTry = false;
                }
            }
        }

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

    static void Main()
    {
        ISummarizer<int> sumFunc = new Calcint();
        ISubtractor<int> subtractFunc = new Calcint();
        IMultiplier<int> multiplierFunc = new Calcint();
        IIntDivider<int> dividerFunc = new Calcint();

        ConsoleProcessor consoleProc = new ConsoleProcessor();
        int x = consoleProc.Read();
        int y = consoleProc.Read();

        sumFunc.Sum(x, y, consoleProc);
        subtractFunc.Subtract(x, y, consoleProc);
        multiplierFunc.Multiply(x, y, consoleProc);
        dividerFunc.Divide(x, y, consoleProc);
    }
}