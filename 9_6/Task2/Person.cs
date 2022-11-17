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