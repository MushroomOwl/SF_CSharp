public class Program {
    class Person {
        public string Fname { get; set; }

        public Person(string fname)
        {
            Fname = fname;
        }

        public void Info() {
            Console.WriteLine(Fname);
        }
    }

    public delegate void CustomEvent();

    class Group
    {
        public int GroupCapacity;
        public List<Person> PersonList;
        public event CustomEvent GroupFullEvent;
        public event CustomEvent OverflowEvent;

        public Group(int groupCapacity)
        {
            GroupCapacity = groupCapacity;
            PersonList = new List<Person>(groupCapacity);
        }

        public virtual void AddPerson(string Fname) {
            if (PersonList.Count >= GroupCapacity) OnGroupOverflow();

            PersonList.Add(new Person(Fname));

            if (PersonList.Count == GroupCapacity) OnGroupFull();
        }

        protected virtual void OnGroupFull()
        {
            GroupFullEvent?.Invoke();
        }

        protected virtual void OnGroupOverflow()
        {
            OverflowEvent?.Invoke();
        }

        // mode = 1 - ACS / mode = 2 = DESC
        public virtual void SortAndListPersons(int mode) {
            int sortModifier = mode == 1 ? 1 : -1;
            PersonList.Sort((Person a, Person b) =>
            {
                return String.Compare(a.Fname, b.Fname) * sortModifier;
            });
            foreach (Person a in PersonList) {
                a.Info();
            }
        }
    }

    static class InputHandler {
        public delegate void Consumer<T>(T input);
        public delegate T Validator<T>(string input);
        private static bool readingInProgress;

        public static void ContiniousReader<T>(string question, Consumer<T> consumer, Validator<T> validator) {
            readingInProgress = true;

            string errorPrefix = "";
            int iteration = 0;

            while (readingInProgress) {
                Console.Write("{0}. {1} ", iteration + 1, question);

                string input = Console.ReadLine();
                T value;
                try
                {
                    value = validator(input);
                    errorPrefix = "";
                }
                catch (Exception ex)
                {
                    errorPrefix = "Bad input. ";
                    continue;
                }
                finally {
                    Console.Write(errorPrefix);
                }
                
                consumer(value);
                iteration++;
            }
        }

        public static void Halt() {
            readingInProgress = false; 
        }
    }

    public static void Main()
    {
        Group people = new Group(2);
        people.GroupFullEvent += OnFullGroupHaltInput;

        InputHandler.ContiniousReader<string>("Please enter persons fname:", people.AddPerson, (str) => {
            if (str.Length < 5) {
                throw new FormatException("Bad string input");
            }
            return str;
        });

        InputHandler.ContiniousReader<int>("Input 1 or 2 to sort group", people.SortAndListPersons, (mode) =>
        {
            int intMode = Convert.ToInt32(mode);
            if (intMode != 1 && intMode != 2)
            {
                throw new FormatException("Bad mode input");
            }
            return intMode;
        });
    }

    public static void OnFullGroupHaltInput()
    {
        Console.WriteLine("Group is full, stopping input sequence");
        InputHandler.Halt();
    }
}