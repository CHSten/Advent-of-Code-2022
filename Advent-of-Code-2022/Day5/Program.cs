using Advent_of_Code_2022;
using System.Diagnostics;

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

            for(int i = 0; i < crateString.Count; i++)
            {
                int idx = i % amountOfCrates;
                crateStacks[idx].Push(crateString[i]);
            }

            foreach (var instruction in instructions)
            {
                string[] str = instruction.Split(new string[] { "move ", " from ", " to " }, StringSplitOptions.RemoveEmptyEntries);
                int amountToMove = int.Parse(str[0]);

                List<string> cratesToMove = new List<string>();

                for (int i = 0; i < amountToMove; i++)
                    cratesToMove.Add(crateStacks[int.Parse(str[1]) - 1].Pop());

                for (int i = 0; i < cratesToMove.Count; i++)
                    crateStacks[int.Parse(str[2]) - 1].Push(cratesToMove[i]);
            }

            Console.WriteLine($"The crates that end at the top of each stack is:\n");
            foreach (var crate in crateStacks)
                Console.WriteLine($"{crate.Peek()}");

        }
    }
}