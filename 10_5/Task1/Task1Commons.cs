namespace Task1Commons
{
    interface IConsoleReader
    {
        int ReadValue();
    }

    class ConsoleReaderInt : IConsoleReader
    {
        public int ReadValue()
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
    }
}