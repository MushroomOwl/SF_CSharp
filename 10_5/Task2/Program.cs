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

    class CalcInt : ISummarizer<int>, ISubtractor<int>, IMultiplier<int>
    {
        int ISummarizer<int>.Sum(int x, int y, ILogger? logger)
        {
            int result = x + y;
            if (logger != null) logger.Event(string.Format("x + y = {0}", result));
            return x + y;
        }
        int ISubtractor<int>.Subtract(int x, int y, ILogger? logger)
        {
            int result = x - y;
            if (logger != null) logger.Event(string.Format("x - y = {0}", result));
            return x - y;
        }
        int IMultiplier<int>.Multiply(int x, int y, ILogger? logger)
        {
            int result = x * y;
            if (logger != null) logger.Event(string.Format("x * y = {0}", result));
            return x * y;
        }
    }

    interface IConsoleReader
    {
        int Read();
    }

    interface ILogger
    {
        void Event(string message);
    }

    class ConsoleProcessor : IConsoleReader, ILogger
    {
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
            Console.WriteLine("LOG: " + message);
        }
    }

    static void Main()
    {
        ISummarizer<int> sumFunc = new CalcInt();
        ISubtractor<int> subtractFunc = new CalcInt();
        IMultiplier<int> multiplierFunc = new CalcInt();

        ConsoleProcessor consoleProc = new ConsoleProcessor();
        int x = consoleProc.Read();
        int y = consoleProc.Read();

        sumFunc.Sum(x, y, consoleProc);
        subtractFunc.Subtract(x, y, consoleProc);
        multiplierFunc.Multiply(x, y, consoleProc);
    }
}