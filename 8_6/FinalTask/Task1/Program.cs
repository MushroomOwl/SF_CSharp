using System.IO;
using static System.Net.WebRequestMethods;

class Program
{
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

        if (!Directory.Exists(folderPath)) {
            Console.WriteLine("Missing directory {0}", folderPath);
            return;
        }

        DirectoryInfo directory;
        DirectoryInfo[] dirs;
        FileInfo[] files;
        try
        {
            directory = new DirectoryInfo(folderPath);
            dirs = directory.GetDirectories();
            files = directory.GetFiles();
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Unexpected file not found exception: {0}", ex.Message);
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Can't access directory: {0}", ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error accessing directory: {0}", ex.Message);
            return;
        }

        DateTime now = DateTime.Now;

        foreach (DirectoryInfo dir in dirs) {
            TimeSpan timeSinceLastAccessed = now - dir.LastAccessTime;
            Console.WriteLine("Directory {0} accessed {1} minutes ago", dir.ToString(), timeSinceLastAccessed.TotalMinutes);
            if (timeSinceLastAccessed.TotalMinutes <= 30) {
                Console.WriteLine("Skipping...");
                continue;
            }
            Console.WriteLine("Removing...");
            Console.ReadLine();
            try
            {
                // It wasn't clear whether in Task 1 we should use recursive function
                // or should check only folder access time. If we should then there's little
                // to nothing difference between Task 1 and Task 3, so just for sake of variation I'll leave it here
                dir.Delete(true);
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Can't completely remove directory - not found exception: {0}", ex.Message);
                continue;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Can't completely remove directory - access denied: {0}", ex.Message);
                continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't completely remove directory - unexpected error: {0}", ex.Message);
                continue;
            }
            Console.WriteLine("Done");
        }

        foreach (FileInfo file in files)
        {
            TimeSpan timeSinceLastAccessed = now - file.LastAccessTime;
            Console.WriteLine("File {0} accessed {1} minutes ago", file.ToString(), timeSinceLastAccessed.TotalMinutes);
            if (timeSinceLastAccessed.TotalMinutes <= 30)
            {
                Console.WriteLine("Skipping...");
                continue;
            }
            Console.ReadLine();
            Console.WriteLine("Removind...", file.ToString());
            try
            {
                file.Delete();
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Can't remove file - not found exception: {0}", ex.Message);
                continue;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Can't remove file - access denied: {0}", ex.Message);
                continue;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't remove file - unexpected error: {0}", ex.Message);
                continue;
            }
            Console.WriteLine("Done");
        }

        Console.WriteLine("Directory cleaned from old files and folders data");
    }
}