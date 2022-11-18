using System.Text.RegularExpressions;
using CustomExceptions;

public class Program
{
    public static void Main()
    {
        Group people = new Group(5);
        people.GroupFullEvent += OnFullGroupHaltInput;

        InputHandler.ContiniousReader<string>("Please enter persons last name:", people.AddPerson, (str) =>
        {
            string pattern = "[^a-zA-Z\\-]";

            if (str.Length < 3)
            {
                Exception ex = new FnameIsTooShortException();
                ex.Data.Add("Min fname length", 3);
                throw ex;
            }
            if (str.Length > 10)
            {
                Exception ex = new FnameIsTooLongException();
                ex.Data.Add("Max fname length", 10);
                throw ex;
            }

            Match invalidSymbolsFound = Regex.Match(str, pattern);
            if (invalidSymbolsFound.Success)
            {
                Exception ex = new InvalidSymbolInFname();
                ex.Data.Add("Invalid symbol", "\"" + invalidSymbolsFound.Value + "\"");
                ex.Data.Add("Allowed symbols", "A-Z, a-z, \"-\"");
                throw ex;
            }

            return str;
        });

        Console.WriteLine("===========================================================================");

        InputHandler.ContiniousReader<int>("Input 1 or 2 to sort group (type \"quit\" to exit):", people.InitiateSort, (mode) =>
        {
            int intMode;
            try
            {
                intMode = Convert.ToInt32(mode);
            }
            catch (Exception ex)
            {
                if (mode == "quit")
                {
                    OnInputLoopInteruption();
                    throw new InputInterruptedException();
                }
                if (ex is FormatException)
                {
                    throw new IncorrectSortMode("Sort mode should be number.");
                }
                else
                {
                    throw new IncorrectSortMode("Cannot convert sort mode value - unexpected error.");
                }
            }
            if (intMode != 1 && intMode != 2)
            {
                throw new IncorrectSortMode("Sort mode should be either 1 or 2.");
            }
            return intMode;
        });
    }

    public static void OnFullGroupHaltInput()
    {
        Console.WriteLine("Group is full, stopping input sequence.");
        InputHandler.Halt();
    }

    public static void OnInputLoopInteruption()
    {
        Console.WriteLine("Input interrupted.");
        InputHandler.Halt();
    }
}