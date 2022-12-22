using System.Diagnostics;

class Program
{
    const string fileName = "Text1.txt";
    const int iterations = 20;
    static Stopwatch stopWatch = new Stopwatch();

    static void Main()
    {
        byte[] data = new byte[0];
        try
        {
            data = File.ReadAllBytes(fileName);
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

        ShowTimeEstimation("List.Add", data, TimeForListAdd);
        ShowTimeEstimation("LinkedList.AddFirst", data, TimeForLinkedListAddFirst);
        ShowTimeEstimation("LinkedList.AddLast", data, TimeForLinkedListAddLast);

        // Extremely slow
        // ShowTimeEstimation("List.Insert", data, TimeForListInsertAfterFirst);

        ShowTimeEstimation("LinkedList.AddAfter", data, TimeForLinkedListAddAfter);
    }

    static void ShowTimeEstimation(string estimatedFunc, byte[] data, Func<byte[], double> func) {
        double[] timesArr = new double[iterations];
        for (int i = 0; i < iterations; i++)
        {
            timesArr[i] = func(data);
        }

        Console.WriteLine("{0} ({1} elements) time(ms): ", estimatedFunc, data.Length);
        Console.WriteLine("\t Total:  {0}", Total(timesArr));
        Console.WriteLine("\t Avg:    {0}", Average(timesArr));
        Console.WriteLine("\t Median: {0}", Median(timesArr));
    }

    static double Total(double[] arr)
    {
        double total = 0;
        for (int i = 0; i < arr.Length; i++)
        {
            total += arr[i];
        }
        return total;
    }

    static double Average(double[] arr) {
        return Total(arr) / arr.Length;
    }

    static double Median(double[] arr)
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