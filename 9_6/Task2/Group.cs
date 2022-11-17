using CustomDelegates;
using CustomExceptions;

class Group
{
    public int GroupCapacity { get; set; }

    protected List<Person> personList;

    public event CustomEvent? GroupFullEvent;
    public event CustomEvent? OverflowEvent;
    public event CustomEvent? SortFinish;
    public event CustomEventIntArg? SortInit;

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