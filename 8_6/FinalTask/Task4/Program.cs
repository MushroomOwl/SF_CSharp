using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
        public string AsStringWithouGroup()
        {
            return string.Format("{0}, {1}", Name, DateOfBirth);
        }
    }

    class Program
    {
        static string DesctopStudentsFolderName = "StudentsDB";

        public static Student[]? ReadStudentsDB(string path)
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    Student[] students = formatter.Deserialize(fs) as Student[];
                    return students;
                }

            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Missing file error: {0}", ex.Message);
                return null;
            }
            catch (AccessViolationException ex)
            {
                Console.WriteLine("Can't access file error: {0}", ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error when accessing file: {0}", ex.Message);
                return null;
            }
        }

        public static void RewriteStudentsGroup(string folder, string group, Student[] students)
        {
            string groupFilePath = string.Format("{0}.txt", Path.Combine(folder, group));
            Console.WriteLine("Writing file {0}", groupFilePath);
            FileInfo groupFile = new FileInfo(groupFilePath);

            try
            {
                groupFile.Delete();
            }
            catch (FileNotFoundException)
            {
                // Do nothing
            }
            catch (AccessViolationException ex)
            {
                Console.WriteLine("Can't rewrite file, no access error: {0}", ex.Message);
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't rewrite file, unexpected error: {0}", ex.Message);
                return;
            }

            using (StreamWriter sw = groupFile.CreateText())
            {
                foreach (Student student in students)
                {
                    sw.WriteLine(student.AsStringWithouGroup());
                }
            }
        }

        public static void Main()
        {
            string? filepath;
            bool firstTry = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(firstTry ? "Input students db file path: " : "Please input correct file path for students DB: ");
                filepath = Console.ReadLine();
                if (filepath == null)
                {
                    firstTry = false;
                }
                else
                {
                    bool isValidFilePath = filepath.IndexOfAny(Path.GetInvalidPathChars()) == -1;
                    if (isValidFilePath)
                    {
                        break;
                    }
                }
            }

            Student[] students = ReadStudentsDB(filepath);
            if (students == null) {
                return;
            }

            Dictionary<string, List<Student>> studentsByGroups = new Dictionary<string, List<Student>>();
            foreach (Student student in students)
            {
                if (!studentsByGroups.ContainsKey(student.Group))
                {
                    studentsByGroups.Add(student.Group, new List<Student>());
                }
                studentsByGroups[student.Group].Add(student);
            }

            string desctopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string studentsFolderPath = Path.Combine(desctopPath, DesctopStudentsFolderName);

            if (!Directory.Exists(studentsFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(studentsFolderPath);
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine("Can't create students db directory, no access error: {0}", ex.Message);
                }
                catch (AccessViolationException ex)
                {
                    Console.WriteLine("Can't create students db directory, no access error: {0}", ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can't create students db directory, unexpected error: {0}", ex.Message);
                    return;
                }
            }

            foreach (KeyValuePair<string, List<Student>> groupStudents in studentsByGroups)
            {
                RewriteStudentsGroup(studentsFolderPath, groupStudents.Key, groupStudents.Value.ToArray());
            }

            Console.WriteLine("Done.");
        }
    }
}

