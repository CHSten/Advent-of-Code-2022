using Advent_of_Code_2022;
using System.Text;

namespace Day3
{
    internal class Program
    {
        public static Dictionary<char, int> Priority = new Dictionary<char, int>();
        static void Main(string[] args)
        {
            string input = Input.Get();
            string[] rucksacks = input.Split("\r\n");

            byte charByte;
            int prioritySum = 0;
            int idx = 1;

            charByte = (byte)'a';
            do
                Priority.Add((char)charByte, idx++);
            while ((char)charByte++ != 'z');

            charByte = (byte)'A';
            do
                Priority.Add((char)charByte, idx++);
            while ((char)charByte++ != 'Z');

            foreach (string rucksack in rucksacks)
            {
                string compartment0 = rucksack.Substring(
                    0, 
                    (int)Math.Round((double)(rucksack.Length / 2), MidpointRounding.ToNegativeInfinity));

                string compartment1 = rucksack.Substring(
                    (int)Math.Round((double)rucksack.Length / 2, MidpointRounding.ToPositiveInfinity),
                    rucksack.Length / 2);

                var common = compartment0.Intersect(compartment1);

                foreach (var character in common)
                    prioritySum += Priority[character];
            }

            Console.WriteLine($"Sum of all the priorities:\n" +
                $"{prioritySum}");
        }
    }
}