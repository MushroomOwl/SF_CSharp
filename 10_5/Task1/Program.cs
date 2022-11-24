class Program {
    interface ISummarizer<T> {
        T Sum(T x, T y);
    }

    interface ISubtractor<T>
    {
        T Subtract(T x, T y);
    }

    interface IMultiplier<T>
    {
        T Multiply(T x, T y);
    }

    class CalcInt : ISummarizer<int>, ISubtractor<int>, IMultiplier<int> {
        int ISummarizer<int>.Sum(int x, int y) {
            return x + y;
        }
        int ISubtractor<int>.Subtract(int x, int y) {
            return x - y;
        }
        int IMultiplier<int>.Multiply(int x, int y) {
            return x * y;
        }
    }

    interface IConsoleReader {
        int ReadValue();
    }

    class ConsoleReaderInt : IConsoleReader {
        public int ReadValue() {
            bool firstTry = true;
            while (true) {
                try {
                    Console.Write(firstTry ? "Input int value: " : "Please input correct integer value: ");
                    string? input = Console.ReadLine();
                    return Convert.ToInt32(input);
                } catch (Exception ex)
                {
                    Console.WriteLine("Input exception - {0}: {1}", ex.GetType(), ex.Message);
                    firstTry = false;
                }
            }
        }
    }

    static void Main()
    {
        ISummarizer<int> sumFunc = new CalcInt();
        ISubtractor<int> subtractFunc = new CalcInt();
        IMultiplier<int> multiplierFunc = new CalcInt();

        IConsoleReader reader = new ConsoleReaderInt();
        int x = reader.ReadValue();
        int y = reader.ReadValue();

        Console.WriteLine("x + y = {0}", sumFunc.Sum(x, y));
        Console.WriteLine("x - y = {0}", subtractFunc.Subtract(x, y));
        Console.WriteLine("x * y = {0}", multiplierFunc.Multiply(x, y));
    }
}