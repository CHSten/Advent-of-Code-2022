using Advent_of_Code_2022;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

namespace Day5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Input.Get();
            List<string> lines = input.Split("\r\n").ToList();
            List<string> instructions = lines.Where(x => x.StartsWith("move")).ToList();
            List<string> crates = lines.Where(x => x.StartsWith("[")).ToList();
            List<Stack<string>> crateStacks = new List<Stack<string>>();
            List<string> crateString = new List<string>();
            int amountOfCrates = (crates[0].Length + 1) / 4;

            foreach (var crate in crates)
                for (int i = 0; i < crate.Length; i += 4)
                    crateString.Add(crate.Substring(i, 3));

            for (int i = 0; i < amountOfCrates; i++)
                crateStacks.Add(new Stack<string>());

            for(int i = crateString.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(crateString[i]))
                    continue;

                int idx = i % amountOfCrates;
                crateStacks[idx].Push(crateString[i]);
            }

            List<Stack<string>> crateStacksNoOrder = DeepCopy(crateStacks);
            List<Stack<string>> crateStacksOrder = DeepCopy(crateStacks);

            foreach (var instruction in instructions)
            {
                string[] str = instruction.Split(new string[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries);
                int amountToMove = int.Parse(str[0]);
                int fromIdx = int.Parse(str[1]) - 1;
                int toIdx = int.Parse(str[2]) - 1;

                for (int i = 0; i < amountToMove; i++)
                    crateStacksNoOrder[toIdx].Push(crateStacksNoOrder[fromIdx].Pop());
            }

            Console.WriteLine($"The crates that end at the top of each stack is:\n");
            foreach (var crate in crateStacksNoOrder)
                Console.WriteLine($"{crate.Peek()}");

            foreach (var instruction in instructions)
            {
                string[] str = instruction.Split(new string[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries);
                int amountToMove = int.Parse(str[0]);
                int fromIdx = int.Parse(str[1]) - 1;
                int toIdx = int.Parse(str[2]) - 1;
                Stack<string> crane = new Stack<string>();

                for (int i = 0; i < amountToMove; i++)
                    crane.Push(crateStacksOrder[fromIdx].Pop());

                crane.Reverse();

                for (int i = 0; i < amountToMove; i++)
                    crateStacksOrder[toIdx].Push(crane.Pop());
            }

            Console.WriteLine($"The crates that end at the top of each stack when order is retained is:\n");
            foreach (var crate in crateStacksOrder)
                Console.WriteLine($"{crate.Peek()}");

        }

        public static T DeepCopy<T>(T item)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, item);
            stream.Seek(0, SeekOrigin.Begin);
            T result = (T)formatter.Deserialize(stream);
            stream.Close();
            return result;
        }
    }
}