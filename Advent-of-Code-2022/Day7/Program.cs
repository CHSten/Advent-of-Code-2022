using Advent_of_Code_2022;
using System.ComponentModel;
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
            public int ID;
            public string Name;
            public int Size;
            public List<Directory> InnerDirectories;
            public Directory? PrevDirectory;
        }

        static void Main(string[] args)
        {
            var input = Input.Get();
            var lines = input.Split("\r\n");
            var currentDirectory = new Directory();
            int newid = 0;

            string previousCommand = null;
            Directory outermostDirectory = new Directory()
            {
                Name = OUTERMOST_DIRECTORY,
                InnerDirectories = new List<Directory>(),
                ID = 0,
                PrevDirectory = null,
            };

            currentDirectory = outermostDirectory;
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(CHANGE_DIRECTORY))
                {
                    var path = lines[i].Substring(CHANGE_DIRECTORY.Length+1);
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
                            ID = ++newid,
                        });
                    else
                    {
                        if (int.Parse(line[0]) >= 100000)
                            currentDirectory.Size += int.Parse(line[0]);
                    }
                }
            }

            int size = 0;
            var queue = new Stack<Directory>();
            queue.Push(outermostDirectory);

            while (queue.Count != 0)
            {

                var directory = queue.Pop();

                for (int i = 0; i < directory.InnerDirectories.Count; i++)
                {
                    directory.Size += directory.InnerDirectories[i].Size;
                }

                foreach (var innerDirectory in directory.InnerDirectories)
                {
                    queue.Push(innerDirectory);
                }

            }

            Console.WriteLine(outermostDirectory.Size);
        }
    }
}