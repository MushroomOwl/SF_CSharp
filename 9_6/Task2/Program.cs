using System;
using System.Collections;
using System.Text.RegularExpressions;

public class Program
{
    class Person
    {
        public string Fname { get; set; }

        public Person(string fname)
        {
            Fname = fname;
        }

        public void Info()
        {
            Console.WriteLine(Fname);
        }
    }

    public delegate void CustomEvent();
    public delegate void CustomEventIntArg(int arg);

    class Group
    {
        public int GroupCapacity { get; set; }

        protected List<Person> personList;

        public event CustomEvent GroupFullEvent;
        public event CustomEvent OverflowEvent;
        public event CustomEvent SortFinish;
        public event CustomEventIntArg SortInit;

        public Group(int groupCapacity)
        {
            GroupCapacity = groupCapacity;
            personList = new List<Person>(groupCapacity);
            
            SortInit += Sort;
            SortFinish += ListPersons;

            OverflowEvent += () =>
            {
                Exception ex = new GroupOverflowException("Can't add more people to group");
                ex.Data.Add("Max grout capacity", groupCapacity);
                throw ex;
            };
        }

        public virtual void AddPerson(string Fname)
        {
            if (personList.Count >= GroupCapacity) OnGroupOverflow();

            personList.Add(new Person(Fname));

            if (personList.Count == GroupCapacity) OnGroupFull();
        }

        protected virtual void OnGroupFull()
        {
            GroupFullEvent?.Invoke();
        }

        protected virtual void OnGroupOverflow()
        {
            OverflowEvent?.Invoke();
        }

        protected virtual void OnSortFinish()
        {
            SortFinish?.Invoke();
        }

        protected virtual void OnSortInit(int mode)
        {
            SortInit?.Invoke(mode);
        }

        public virtual void InitiateSort(int mode)
        {
            OnSortInit(mode);
        }

        // mode = 1 - ACS / mode = 2 = DESC
        public virtual void Sort(int mode)
        {
            int sortModifier = mode == 1 ? 1 : -1;
            personList.Sort((Person a, Person b) =>
            {
                return String.Compare(a.Fname, b.Fname) * sortModifier;
            });
            OnSortFinish();
        }

        public virtual void ListPersons()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Listing persons in group:");
            foreach (Person a in personList)
            {
                a.Info();
            }
            Console.WriteLine("---------------------------------------------------");
        }
    }

    public class IncorrectInput : FormatException
    {
        public IncorrectInput() : base() { }
        public IncorrectInput(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class FnameIsTooLongException : IncorrectInput
    {
        public FnameIsTooLongException() : base() { }
        public FnameIsTooLongException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class FnameIsTooShortException : IncorrectInput
    {
        public FnameIsTooShortException() : base() { }
        public FnameIsTooShortException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class InvalidSymbolInFname : IncorrectInput
    {
        public InvalidSymbolInFname() : base() { }
        public InvalidSymbolInFname(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class IncorrectSortMode : IncorrectInput
    {
        public IncorrectSortMode() : base() { }
        public IncorrectSortMode(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class GroupOverflowException : InvalidOperationException
    {
        public GroupOverflowException() : base() { }
        public GroupOverflowException(string _exceptionMessage) : base(_exceptionMessage) { }
    }

    public class InputInterruptedException : Exception
    {
        public InputInterruptedException() : base() { }
        public InputInterruptedException(string _exceptionMessage) : base(_exceptionMessage) { }
    }


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
                catch (InputInterruptedException) {
                    continue;
                }
                catch (IncorrectSortMode ex) {
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

    public static void Main()
    {
        Group people = new Group(2);
        people.GroupFullEvent += OnFullGroupHaltInput;

        InputHandler.ContiniousReader<string>("Please enter persons fname:", people.AddPerson, (str) =>
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

        InputHandler.ContiniousReader<int>("Input 1 or 2 to sort group (type \"quit\" to exit)", people.InitiateSort, (mode) =>
        {
            int intMode;
            try
            {
                intMode = Convert.ToInt32(mode);
            }
            catch (Exception ex) {
                if (mode == "quit") {
                    OnInputLoopInteruption();
                    throw new InputInterruptedException();
                }
                if (ex is FormatException)
                {
                    throw new IncorrectSortMode("Sort mode should be number.");
                }
                else {
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