using System.Diagnostics;

class Program
{
    const string DefaultFileName = "Text1.txt";
    const int DefaultIterations = 20;
    static Stopwatch stopWatch = new Stopwatch();

    static void Main()
    {
        Console.Write("Input path to text file (press enter for \"Text1.txt\"): ");
        string filename = Console.ReadLine();

        if (filename == null || filename.Length == 0)
        {
            filename = DefaultFileName;
        }

        Console.Write("Input iterations count (press enter for 20): ");
        string iterationsStr = Console.ReadLine();
        bool isNumber = int.TryParse(iterationsStr, out int iterations);
        if (!isNumber) {
            Console.WriteLine("Not a number, using default - {0}", DefaultIterations);
            iterations = DefaultIterations;
        }

        byte[] data = new byte[0];
        try
        {
            data = File.ReadAllBytes(filename);
        }
        catch (Exception ex) {
            Console.WriteLine("Can't read file, got exception:");
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return;
        }

        if (data.Length < 1024) {
            Console.WriteLine("File is too small to perform proper performance test, please provide at least 1KB file.");
            return;
        }
        
        stopWatch.Start();

        ShowTimeEstimation("List.Add", data, iterations, TimeForListAdd);
        ShowTimeEstimation("LinkedList.AddFirst", data, iterations, TimeForLinkedListAddFirst);
        ShowTimeEstimation("LinkedList.AddLast", data, iterations, TimeForLinkedListAddLast);

        // Extremely
        // ShowTimeEstimation("List.Insert", data, TimeForListInsertAfterFirst);

        ShowTimeEstimation("LinkedList.AddAfter", data, iterations, TimeForLinkedListAddAfter);
    }

    static void ShowTimeEstimation(string estimatedFunc, byte[] data, int iterations, Func<byte[], double> func) {
        double[] timesArr = new double[iterations];
        for (int i = 0; i < iterations; i++)
        {
            timesArr[i] = func(data);
        }

        Console.WriteLine("{0} ({1} elements) time(ms): ", estimatedFunc, data.Length);
        double total = ArraySum(timesArr);
        double avg = ArrayAverage(timesArr);
        double median = ArrayMedian(timesArr);
        Console.WriteLine("\t Total:  {0} / {1} per symbol", total, total / data.Length);
        Console.WriteLine("\t Avg:    {0} / {1} per symbol", avg, avg / data.Length);
        Console.WriteLine("\t Median: {0} / {1} per symbol", median, median / data.Length);
    }

    static double ArraySum(double[] arr)
    {
        double total = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            total += arr[i];
        }
        return total;
    }

    static double ArrayAverage(double[] arr) {
        return ArraySum(arr) / arr.Length;
    }

    static double ArrayMedian(double[] arr)
    {
        return arr[arr.Length / 2];
    }

    static double TimeForListAdd(byte[] data)
    {
        List<byte> list = new List<byte>();

        stopWatch.Restart();
        for (int i = 0; i < data.Length; i++)
        {
            list.Add(data[i]);
        }
        stopWatch.Stop();

        list.Clear();

        return stopWatch.Elapsed.TotalMilliseconds;
    }

    static double TimeForListInsertAfterFirst(byte[] data)
    {
        List<byte> list = new List<byte>();
        list.Add(0);

        stopWatch.Restart();
        for (int i = 0; i < data.Length; i++)
        {
            list.Insert(1,data[i]);
        }
        stopWatch.Stop();

        list.Clear();

        return stopWatch.Elapsed.TotalMilliseconds;
    }

    static double TimeForLinkedListAddAfter(byte[] data)
    {
        LinkedList<byte> list = new LinkedList<byte>();
        list.AddFirst(0);

        stopWatch.Restart();
        for (int i = 0; i < data.Length; i++)
        {
            list.AddAfter(list.First, data[i]);
        }
        stopWatch.Stop();

        list.Clear();

        return stopWatch.Elapsed.TotalMilliseconds;
    }

    static double TimeForLinkedListAddFirst(byte[] data)
    {
        LinkedList<byte> list = new LinkedList<byte>();

        stopWatch.Restart();
        for (int i = 0; i < data.Length; i++)
        {
            list.AddFirst(data[i]);
        }
        stopWatch.Stop();

        list.Clear();

        return stopWatch.Elapsed.TotalMilliseconds;
    }

    static double TimeForLinkedListAddLast(byte[] data)
    {
        LinkedList<byte> list = new LinkedList<byte>();

        stopWatch.Restart();
        for (int i = 0; i < data.Length; i++)
        {
            list.AddLast(data[i]);
        }
        stopWatch.Stop();

        list.Clear();

        return stopWatch.Elapsed.TotalMilliseconds;
    }
}