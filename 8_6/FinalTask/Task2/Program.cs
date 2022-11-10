using System.Drawing;
using System.IO;

class Program
{
    public static long CalcSize(FileInfo file)
    {
        return file.Length;
    }

    public static long CalcSize(DirectoryInfo directory) {
        long size = 0;
        DirectoryInfo[] dirs = directory.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            size += CalcSize(dir);
        }
        FileInfo[] files = directory.GetFiles();
        foreach (FileInfo file in files)
        {
            size += CalcSize(file);
        }
        return size;
    }

    public static long CalcSize(string path)
    {
        try {
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                return CalcSize(dir);
            }
            else
            {
                FileInfo file = new FileInfo(path);
                return CalcSize(file);
            }
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Can't get size of {0} - directory not found: {1}", ex.Message);
            return 0;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Can't get size of {0} - file not found: {1}", ex.Message);
            return 0;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Can't get size of {0} - unexpected error: {1}", ex.Message);
            return 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Can't get size of {0} - unexpected error: {1}", ex.Message);
            return 0;
        }
    }

    public static void Main()
    {
        string? folderPath;
        bool firstTry = true;
        while (true)
        {
            Console.Clear();
            Console.Write(firstTry ? "Input path to folder to clean: " : "Please input correct path to folder: ");
            folderPath = Console.ReadLine();
            if (folderPath == null)
            {
                firstTry = false;
            }
            else
            {
                bool isValidFilePath = folderPath.IndexOfAny(Path.GetInvalidPathChars()) == -1;
                if (isValidFilePath)
                {
                    break;
                }
            }
        }

        long obrainableSize = CalcSize(folderPath);

        Console.WriteLine("Estimated size is {0} bytes", obrainableSize);
    }
}