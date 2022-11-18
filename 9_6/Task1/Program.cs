using CreatureCommons;
using CreatureExceptions;
using System.Collections;

public class Program
{
    public static void Main()
    {
        // Practice with 5 different exceptions
        // -----------------------------------------------------------------------------------------
        Exception[] exs = new Exception[5];

        exs[0] = new Exception("Fatal error");
        exs[0].HelpLink = @"https://en.wikipedia.org/wiki/Blue_screen_of_death";

        exs[1] = new ArgumentException("Invalid argument exception");
        exs[1].Data.Add("Allowed arguments are", string.Join(", ", new int[] { 1, 2, 3 }));

        exs[2] = new FileNotFoundException("Missing file");
        exs[2].Data.Add("Missing file path", @"C:\Folder\File.data");

        exs[3] = new TimeoutException("Operation terminated due to timeout");
        exs[3].Data.Add("Timeout (ms)", 20000);
        exs[3].Data.Add("Operation time (ms)", 23412);

        exs[4] = new CatsCantFlyException("Cat's can't fly");
        exs[4].HelpLink = @"https://en.wikipedia.org/wiki/Cat";

        foreach (Exception exception in exs)
        {
            string errorInfo = "";
            try
            {
                throw exception;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case CatsCantFlyException:
                        errorInfo += "CatsCantFlyException exception\n";
                        break;
                    case ArgumentException:
                        errorInfo += "ArgumentException exception\n";
                        break;
                    case FileNotFoundException:
                        errorInfo += "FileNotFoundException exception\n";
                        break;
                    case TimeoutException:
                        errorInfo += "TimeoutException exception\n";
                        break;
                    case Exception:
                        errorInfo += "General exceptuon\n";
                        break;
                }
                errorInfo += "Message: " + ex.Message;
                if (ex.HelpLink != null && ex.HelpLink.Length > 0)
                {
                    errorInfo += "\nHelp link: " + ex.HelpLink;
                }
                if (ex.Data.Count > 0)
                {
                    errorInfo += "\nData:";
                    foreach (DictionaryEntry data in ex.Data)
                    {
                        errorInfo += "\n\t" + data.Key + ": " + data.Value;
                    }
                }
            }
            finally
            {
                Console.WriteLine(errorInfo);
                Console.WriteLine("\n-------------------------------------------\n");
            }
        }
        // -----------------------------------------------------------------------------------------

        Console.WriteLine("\n===========================================\n");

        // Practice with exceptions - application of custom exceptions 
        // -----------------------------------------------------------------------------------------
        try
        {
            Cat cat = new Cat();
            cat.HasTail = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ExceptionInfo(ex));
            Console.WriteLine("\n-------------------------------------------\n");
        }

        try
        {
            Cow cow = new Cow();
            cow.HasTail = true;
            cow.Diet = new Food[] { Food.Vegetables, Food.Meat };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ExceptionInfo(ex));
            Console.WriteLine("\n-------------------------------------------\n");
        }

        try
        {
            Insect bug = new Insect();
            bug.LimbsCount = 7;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ExceptionInfo(ex));
            Console.WriteLine("\n-------------------------------------------\n");
        }

        try
        {
            Human human = new Human();
            human.HasTail = false;
            human.Diet = new Food[] { Food.Vegetables, Food.Meat };
            human.Flying = false;
            human.LimbsCount = 4;
            human.Info();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ExceptionInfo(ex));
            Console.WriteLine("\n-------------------------------------------\n");
        }
        // -----------------------------------------------------------------------------------------
    }

    static public string ExceptionInfo(Exception ex)
    {
        string errorInfo = ex.GetType().Name;
        errorInfo += "\nMessage: " + ex.Message;
        if (ex.HelpLink != null && ex.HelpLink.Length > 0)
        {
            errorInfo += "\nHelp link: " + ex.HelpLink;
        }
        if (ex.Data.Count > 0)
        {
            errorInfo += "\nData:";
            foreach (DictionaryEntry data in ex.Data)
            {
                errorInfo += "\n\t" + data.Key + ": " + data.Value;
            }
        }
        return errorInfo;
    }
}