using System.Diagnostics;

class Program
{
    const string DefaultFileName = "Text1.txt";

    static void Main()
    {
        Console.Write("Input path to text file (press enter for \"Text1.txt\"): ");
        string filename = Console.ReadLine();

        if (filename == null || filename.Length == 0)
        {
            filename = DefaultFileName;
        }

        string[] lines = new string[0];
        try
        {
            lines = File.ReadAllLines(filename);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Can't read file, got exception:");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return;
        }

        Dictionary<string, int> words = new Dictionary<string, int>();

        foreach (string line in lines)
        {
            string clearLine = new string(line.Where(c => !char.IsPunctuation(c)).ToArray());
            foreach (string word in clearLine.Trim().Split(" "))
            {
                if (word.Length == 0) {
                    continue;
                }
                if (!words.ContainsKey(word))
                {
                    words.Add(word, 0);
                }
                words[word]++;
            }
        }

        int idx = 0;
        Console.WriteLine("10 Most often words in text: ");
        foreach (KeyValuePair<string, int> elem in words.OrderByDescending(elem => elem.Value)) {
            idx++;
            if (idx > 10) {
                break;
            }
            Console.WriteLine("{0}. {1} - {2} times", idx, elem.Key, elem.Value);
        }
    }
}