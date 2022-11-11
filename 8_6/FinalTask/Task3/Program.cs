class Program
{
    abstract public class DataProcessor {
        abstract public void Exec(FileInfo file);
        abstract public void Exec(DirectoryInfo dir, DateTime? lastAccess);
        abstract public void PrintProgressReport();
    }

    public class Cleaner: DataProcessor {
        private int removedFiles;
        private int failedToRemoveFiles;
        private int skippedFiles;
        private int removedFolders;
        private int failedToRemoveFolders;
        private int skippedFolders;
        private long fullSize;
        private long cleanedSize;
        private int fileTTLmin = 30;

        public Cleaner(int ttl) {
            removedFiles = 0; 
            failedToRemoveFiles = 0;
            skippedFiles = 0;
            removedFolders = 0;
            failedToRemoveFolders = 0;
            skippedFolders = 0;
            fullSize = 0;
            cleanedSize = 0;
            if (ttl > 0) {
                fileTTLmin = ttl;
            }
        }
        public override void Exec(FileInfo file) {
            long fileSize = file.Length;
            fullSize += fileSize;
            TimeSpan timeSinceLastAccessed = DateTime.Now - file.LastAccessTime;
            if (timeSinceLastAccessed.TotalMinutes <= fileTTLmin)
            {
                skippedFiles++;
                return;
            }
            try
            {                
                file.Delete();
                removedFiles++;
                cleanedSize += fileSize;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Cleaner can't remove {0} - directory not found: {1}", file.ToString(), ex.Message);
                failedToRemoveFiles++;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Cleaner can't remove {0} - access denied: {1}", file.ToString(), ex.Message);
                failedToRemoveFiles++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cleaner can't remove {0} - unexpected error: {1}", file.ToString(), ex.Message);
                failedToRemoveFiles++;
            }
        }
        public override void Exec(DirectoryInfo dir, DateTime? lastAccess) {
            TimeSpan timeSinceLastAccessed = DateTime.Now - dir.LastAccessTime;
            if (lastAccess != null) {
                timeSinceLastAccessed = DateTime.Now - (DateTime)lastAccess;
            }
            if (timeSinceLastAccessed.TotalMinutes <= fileTTLmin)
            {
                skippedFolders++;
                return;
            }
            try
            {
                dir.Delete();
                removedFolders++;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Cleaner can't remove {0} - directory not found: {1}", dir.ToString(), ex.Message);
                failedToRemoveFolders++;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Cleaner can't remove {0} - access denied: {1}", dir.ToString(), ex.Message);
                failedToRemoveFolders++;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cleaner can't remove {0} - unexpected error: {1}", dir.ToString(), ex.Message);
                failedToRemoveFolders++;
            }
        }
        public override void PrintProgressReport() {
            Console.WriteLine("CleanerProgressReport:");
            Console.WriteLine("\tEstimated folder size before cleaning: {0}", fullSize);
            Console.WriteLine("\tEstimated cleaned content size cleaning: {0}", cleanedSize);
            Console.WriteLine("\tEstimated folder size after cleaning: {0}", fullSize - cleanedSize);

            Console.WriteLine("\tFiles removed: {0}", removedFiles);
            Console.WriteLine("\tFiles failed to remove: {0}", failedToRemoveFiles);
        }
    }

    public static void FolderWalk(FileInfo file, DataProcessor proc)
    {
        proc.Exec(file);
    }

    public static void FolderWalk(DirectoryInfo directory, DataProcessor proc)
    {
        DirectoryInfo[] dirs = directory.GetDirectories();
        foreach (DirectoryInfo dir in dirs)
        {
            // Little messed up here, trying to delete dir
            // after it was accessed so access time is almost now
            // and dir won't be removed. Therefore here's this stub.
            DateTime lastAccess = dir.LastAccessTime;
            FolderWalk(dir, proc);
            proc.Exec(dir, lastAccess);
        }

        FileInfo[] files = directory.GetFiles();
        foreach (FileInfo file in files)
        {
            FolderWalk(file, proc);
        }
    }

    public static void FolderWalk(string path, DataProcessor proc)
    {
        try
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                FolderWalk(dir, proc);
                return;
            }
            else
            {
                FileInfo file = new FileInfo(path);
                FolderWalk(file, proc);
                return;
            }
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine("Can't access {0} - directory not found: {1}", path, ex.Message);
            return;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Can't access {0} - file not found: {1}", path, ex.Message);
            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Can't access {0} - access error: {1}", path, ex.Message);
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Can't access {0} - unexpected error: {1}", path, ex.Message);
            return;
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

        Cleaner cleanerProc = new Cleaner(30);
        FolderWalk(folderPath, cleanerProc);

        cleanerProc.PrintProgressReport();

        Console.WriteLine("Done.");
    }
}