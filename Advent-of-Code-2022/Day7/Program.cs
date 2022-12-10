using Advent_of_Code_2022;
using System.ComponentModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Day7
{
    internal class Program
    {
        public const string CHANGE_DIRECTORY = "$ cd",
            MOVE_OUT = "..",
            OUTERMOST_DIRECTORY = "/",
            LIST = "$ ls";
        public class Directory
        {
            public string Name;
            public long Size;
            public List<Directory> InnerDirectories;
            public Directory? PrevDirectory;
        }

        static void Main(string[] args)
        {
            var input = Input.Get();
            var lines = input.Split("\r\n");
            var currentDirectory = new Directory();

            string previousCommand = null;
            Directory outermostDirectory = new Directory()
            {
                Name = OUTERMOST_DIRECTORY,
                InnerDirectories = new List<Directory>(),
                PrevDirectory = null,
            };

            //Setup
            currentDirectory = outermostDirectory;
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(CHANGE_DIRECTORY))
                {
                    var path = lines[i].Substring(CHANGE_DIRECTORY.Length + 1);

                    previousCommand = lines[i];

                    if (path == MOVE_OUT)
                    {
                        currentDirectory = currentDirectory.PrevDirectory;
                        continue;
                    }
                    else
                        currentDirectory = currentDirectory.InnerDirectories.Single(x => x.Name == path);

                }
                else if (lines[i].StartsWith(LIST))
                {
                    previousCommand = lines[i];
                    continue;
                }
                else if (previousCommand.StartsWith(LIST))
                {
                    var line = lines[i].Split(" ");

                    if (line[0].StartsWith("dir"))
                        currentDirectory.InnerDirectories.Add(new Directory()
                        {
                            Name = line[1],
                            InnerDirectories = new List<Directory>(),
                            PrevDirectory = currentDirectory,
                        });
                    else
                        currentDirectory.Size += long.Parse(line[0]);
                }
            }

            var queue = new Stack<Directory>();
            List<Directory> allDirectories = new List<Directory>();
            queue.Push(outermostDirectory);

            while (queue.Count != 0)
            {
                var directory = queue.Pop();
                allDirectories.Add(directory);

                var newDirectory = directory;
                while (newDirectory.PrevDirectory != null)
                {
                    newDirectory.PrevDirectory.Size += directory.Size;
                    newDirectory = newDirectory.PrevDirectory;
                }

                foreach (var innerDirectory in directory.InnerDirectories)
                    queue.Push(innerDirectory);
            }

            //Part1
            long sumOfAllDirectorySizes = 0;
            foreach (var directory in allDirectories)
            {
                if (directory.Size > 100000)
                    continue;

                sumOfAllDirectorySizes += directory.Size;
            }
            Console.WriteLine($"The sum of all the directories is:" +
                $"{sumOfAllDirectorySizes}");

            //Part2
            long totalSpace = 70000000;
            long updateSpace = 30000000;
            long unusedSpace = totalSpace - outermostDirectory.Size;
            long minRequiredSpace = updateSpace - unusedSpace;

            Directory directoryToDelete = outermostDirectory;
            foreach (var directory in allDirectories)
            {
                if (directory.Size >= minRequiredSpace && directory.Size < directoryToDelete.Size)
                    directoryToDelete = directory;
            }

            Console.WriteLine($"the size of the directory which frees up enough space:" +
                $"{directoryToDelete.Size}");
        }
    }
}