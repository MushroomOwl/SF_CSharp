class Person
{
    public string LastName { get; set; }

    public Person(string lname)
    {
        LastName = lname;
    }

    public void Info()
    {
        Console.WriteLine(LastName);
    }
}