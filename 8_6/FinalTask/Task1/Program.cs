class Program
{
    public static void RecursiveRemove(DirectoryInfo directory)
    {
        DateTime now = DateTime.Now;
        DirectoryInfo[] dirs = directory.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            RecursiveRemove(dir);
            TimeSpan timeSinceLastAccessed = now - dir.LastAccessTime;
            Console.WriteLine("Directory {0} accessed {1} minutes ago", dir.ToString(), timeSinceLastAccessed.TotalMinutes);
            if (timeSinceLastAccessed.TotalMinutes <= 1)
            {
                Console.WriteLine("Skipping...");
                continue;
            }
            Console.WriteLine("Removing...");
            try
            {
                dir.Delete();
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

        FileInfo[] files = directory.GetFiles();
        foreach (FileInfo file in files)
        {
            TimeSpan timeSinceLastAccessed = now - file.LastAccessTime;
            Console.WriteLine("File {0} accessed {1} minutes ago", file.ToString(), timeSinceLastAccessed.TotalMinutes);
            if (timeSinceLastAccessed.TotalMinutes <= 1)
            {
                Console.WriteLine("Skipping...");
                continue;
            }
            Console.WriteLine("Removing...", file.ToString());
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

        if (!Directory.Exists(folderPath)) {
            Console.WriteLine("Missing directory {0}", folderPath);
            return;
        }

        DirectoryInfo directory;
        try
        {
            directory = new DirectoryInfo(folderPath);
            RecursiveRemove(directory);
            Console.WriteLine("Directory cleaned from old files and folders data");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Unexpected file not found exception: {0}", ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Can't access directory: {0}", ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error accessing directory: {0}", ex.Message);
        }
    }
}