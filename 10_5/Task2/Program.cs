using Task2Commons;
using Task2Calculation;

class Program
{
    static void Main()
    {
        IConsoleReader consoleProc = new ConsoleReader();
        ILogger logger = new Logger();

        // Creating class instance for every function
        ISummarizer<int> sumFunc = new IntCalc();
        ISubtractor<int> subtractFunc = new IntCalc();
        IMultiplier<int> multiplierFunc = new IntCalc();
        IIntDivider<int> dividerFunc = new IntCalc();

        consoleProc.Read(out int x, logger, "x");
        consoleProc.Read(out int y, logger, "y");

        sumFunc.Sum(x, y, logger);
        subtractFunc.Subtract(x, y, logger);
        multiplierFunc.Multiply(x, y, logger);
        dividerFunc.Divide(x, y, logger);

        Console.WriteLine("------------------------------------------------------");

        // Using single class instance to execute all functions
        consoleProc.Read(out x, logger, "x");
        consoleProc.Read(out y, logger, "y");

        IntCalc calc = new IntCalc();
        ((ISummarizer<int>)calc).Sum(x, y, logger);
        ((ISubtractor<int>)calc).Subtract(x, y, logger);
        ((IMultiplier<int>)calc).Multiply(x, y, logger);
        ((IIntDivider<int>)calc).Divide(x, y, logger);
    }
}