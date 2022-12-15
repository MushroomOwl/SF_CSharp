class User
{
    public string Login { get; set; }
    public string Name { get; set; }
    public bool IsPremium { get; set; }

    public User(string login, string name, bool isPremium)
    {
        Login = login;
        Name = name;
        IsPremium = isPremium;
    }
}

public class Program
{
    public static void Main()
    {
        List<User> users = new List<User>();

        users.Add(new User("admin", "Andrew", true));
        users.Add(new User("manager", "Lisa", true));
        users.Add(new User("user", "Paul", false));
        users.Add(new User("guest", "Martha", false));

        ShowGreetings(users);
    }

    static void ShowGreetings(List<User> users)
    {
        bool nextLogin = true;

        while (nextLogin)
        {
            Console.Write("Input login: ");
            string? login = Console.ReadLine();

            if (login == null || login.Length == 0)
            {
                Console.WriteLine("Login cannot be empty");
            }
            else
            {
                User? user = users.Find((u) => u.Login == login);
                if (user == null)
                {
                    Console.WriteLine("No such user");
                }
                else
                {
                    if (!user.IsPremium)
                    {
                        ShowAds();
                    }
                    Console.WriteLine("Hello {0}!", user.Name);
                }
            }

            Console.Write("Enter new login? (Enter \"y\" if yes): ");
            string? nextLoginStr = Console.ReadLine();

            if (nextLoginStr != "y")
            {
                nextLogin = false;
            }
        }
    }

    static void ShowAds()
    {
        Console.WriteLine("Visit our new site with free games free.games.for.a.fool.com!");
        Thread.Sleep(1000);

        Console.WriteLine("Buy subscription for WeCombo and listen music anytime anywhere!");
        Thread.Sleep(2000);

        Console.WriteLine("Subscribe to Premium to remove ads!");
        Thread.Sleep(3000);
    }
}