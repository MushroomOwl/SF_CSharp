class Program
{
    public static void Main()
    {
        string? filepath;
        bool firstTry = true;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(firstTry ? "Input path to folder to clean: " : "Please input correct path to folder: ");
            filepath = Console.ReadLine();
            if (filepath == null)
            {
                firstTry = false;
            }
            else
            {
                bool isValidFilePath = filepath.IndexOfAny(Path.GetInvalidPathChars()) == -1;
                if (isValidFilePath)
                {
                    break;
                }
            }
        }
    }
}