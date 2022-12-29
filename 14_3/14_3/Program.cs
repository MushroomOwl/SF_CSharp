using System.Text;

namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            // For cmd to show cyrilic symbols
            Console.OutputEncoding = Encoding.UTF8;

            var phoneBook = new List<Contact>();

            phoneBook.Add(new Contact("Игорь", "Николаев", 79990000001, "igor@example.com"));
            phoneBook.Add(new Contact("Сергей", "Довлатов", 79990000010, "serge@example.com"));
            phoneBook.Add(new Contact("Анатолий", "Карпов", 79990000011, "anatoly@example.com"));
            phoneBook.Add(new Contact("Валерий", "Леонтьев", 79990000012, "valera@example.com"));
            phoneBook.Add(new Contact("Сергей", "Брин", 799900000013, "serg@example.com"));
            phoneBook.Add(new Contact("Иннокентий", "Смоктуновский", 799900000013, "innokentii@example.com"));

            var sortedPhoneBook = from s in phoneBook orderby s.Name, s.LastName select s;

            while (true)
            {
                Console.Write("Введите номер страницы: ");
                var input = Console.ReadKey().KeyChar;

                var parsed = int.TryParse(input.ToString(), out int pageNumber);

                if (!parsed || pageNumber < 1 || pageNumber > 3)
                {
                    Console.WriteLine("\nСтраницы не существует\n");
                }

                else
                {
                    var pageContent = sortedPhoneBook.Skip((pageNumber - 1) * 2).Take(2);
                    Console.WriteLine();

                    foreach (var entry in pageContent)
                        Console.WriteLine(entry.Name + " " + entry.LastName + ": " + entry.PhoneNumber);

                    Console.WriteLine();
                }
            }
        }
    }
}