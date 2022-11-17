using CustomExceptions;
using System.Collections;

static class InputHandler
{
    public delegate void Consumer<T>(T input);
    public delegate T Validator<T>(string input);
    private static bool readingInProgress;

    public static void ContiniousReader<T>(string question, Consumer<T> consumer, Validator<T> validator)
    {
        readingInProgress = true;

        string error = "";
        int iteration = 0;

        while (readingInProgress)
        {
            Console.Write("{0}. {1} ", iteration + 1, question);

            string input = Console.ReadLine();
            T value;
            try
            {
                value = validator(input);
                error = "";
                consumer(value);
                iteration++;
            }
            catch (InputInterruptedException)
            {
                error = "";
                continue;
            }
            catch (IncorrectSortMode ex)
            {
                error = ex.Message;
            }
            catch (IncorrectInput ex)
            {
                error = ex.Message;

                switch (ex)
                {
                    case InvalidSymbolInFname:
                        error = "Bad input. Invalid symbol.";
                        break;
                    case FnameIsTooLongException:
                        error = "Bad input. Fname is too long.";
                        break;
                    case FnameIsTooShortException:
                        error = "Bad input. Fname is too short.";
                        break;
                    default:
                        error = "Bad input. ";
                        break;
                }

                foreach (DictionaryEntry data in ex.Data)
                {
                    error += String.Format("\n{0}: {1}", data.Key, data.Value);
                }
            }
            catch (FormatException ex)
            {
                error = "Unhandled format error.";
            }
            catch (Exception ex)
            {
                error = "Unexpected error: " + ex.Message;
                continue;
            }
            finally
            {
                if (error.Length > 0) Console.WriteLine(error);
            }
        }
    }

    public static void Halt()
    {
        readingInProgress = false;
    }
}