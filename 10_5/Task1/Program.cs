using Task1Commons;
using Task1Calculation;

class Program
{
    static void Main()
    {
        // Creating class instance for every function
        ISummarizer<int> sumFunc = new IntCalc();
        ISubtractor<int> subtractFunc = new IntCalc();
        IMultiplier<int> multiplierFunc = new IntCalc();

        IConsoleReader reader = new ConsoleReaderInt();
        int x = reader.ReadValue();
        int y = reader.ReadValue();

        Console.WriteLine("x + y = {0}", sumFunc.Sum(x, y));
        Console.WriteLine("x - y = {0}", subtractFunc.Subtract(x, y));
        Console.WriteLine("x * y = {0}", multiplierFunc.Multiply(x, y));

        Console.WriteLine("------------------------------------------------------");

        // Using single class instance to execute all functions
        x = reader.ReadValue();
        y = reader.ReadValue();

        IntCalc calc = new IntCalc();

        Console.WriteLine("x + y = {0}", ((ISummarizer<int>)calc).Sum(x, y));
        Console.WriteLine("x - y = {0}", ((ISubtractor<int>)calc).Subtract(x, y));
        Console.WriteLine("x * y = {0}", ((IMultiplier<int>)calc).Multiply(x, y));
    }
}