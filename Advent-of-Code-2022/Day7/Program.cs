using Advent_of_Code_2022;

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
            public int Size;
            public List<string> NextDirectories;
        }

        static void Main(string[] args)
        {
            var input = Input.Get();
            var lines = input.Split("\r\n");
            var directories = new List<Directory>();
            var currentIdx = 0;

            string previousCommand = null;

            directories.Add(new Directory()
            {
                Name = OUTERMOST_DIRECTORY,
                NextDirectories = new List<string>(),
            });
            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].StartsWith(CHANGE_DIRECTORY))
                {
                    var path = lines[i].Substring(CHANGE_DIRECTORY.Length);

                    if (path == MOVE_OUT)
                    {
                        currentIdx--;
                        continue;
                    }

                    previousCommand = lines[i];
                    var directory = new Directory()
                    {
                        Name = path,
                        NextDirectories = new List<string>(),
                        Size = 0,
                    };
                    directories.Add(directory);
                    currentIdx++;
                }
                else if (lines[i].StartsWith(LIST))
                {
                    previousCommand = lines[i];
                    continue;
                }

                if (previousCommand.StartsWith(LIST))
                {
                    var line = lines[i].Split(" ");

                    if (line[0].StartsWith("dir"))
                        directories[currentIdx].NextDirectories.Add(line[1]);
                    else
                        directories[currentIdx].Size += int.Parse(line[0]);
                }
            }
        }
    }
}