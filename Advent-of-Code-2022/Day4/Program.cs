using Advent_of_Code_2022;

namespace Day4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Input.Get();
            string[] pairs = input.Split("\r\n");
            int containPairs = 0;
            int overlappingPairs = 0;

            foreach (var pair in pairs)
            {
                string[] elfAssignments = pair.Split(',');
                string[] elf0Assigment = elfAssignments[0].Split('-');
                string[] elf1Assigment = elfAssignments[1].Split('-');

                if (int.Parse(elf0Assigment[0]) <= int.Parse(elf1Assigment[0]) &&
                    int.Parse(elf0Assigment[1]) >= int.Parse(elf1Assigment[1]))
                    containPairs++;
                else if (int.Parse(elf1Assigment[0]) <= int.Parse(elf0Assigment[0]) &&
                         int.Parse(elf1Assigment[1]) >= int.Parse(elf0Assigment[1]))
                    containPairs++;
            }

            Console.WriteLine($"In {containPairs} does one pairs range contain the other");

            foreach (var pair in pairs)
            {
                string[] elfAssignments = pair.Split(',');
                string[] elf0Assigment = elfAssignments[0].Split('-');
                string[] elf1Assigment = elfAssignments[1].Split('-');

                if (int.Parse(elf0Assigment[1]) < int.Parse(elf1Assigment[0]) ||
                    int.Parse(elf0Assigment[0]) > int.Parse(elf1Assigment[1]))
                    continue;

                overlappingPairs++;
            }

            Console.WriteLine($"In {overlappingPairs} does one pairs range overlap the other");
        }
    }
}