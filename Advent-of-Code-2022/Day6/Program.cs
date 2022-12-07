using Advent_of_Code_2022;

namespace Day6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Input.Get();

            Console.WriteLine($"number of characters processed in marker:\n" +
                $"{distinctCharacters(input, 4)}");

            Console.WriteLine($"number of characters processed in start-of-message marker:\n" +
                $"{distinctCharacters(input, 14)}");
        }

        private static int distinctCharacters(string input, int amountOfDistinctChar)
        {
            for (int i = amountOfDistinctChar; i < input.Length; i++)
            {
                char[] characters = new char[amountOfDistinctChar];
                for (int c = 0; c < amountOfDistinctChar; c++)
                {
                    int idx = i - c - 1;
                    characters[c] = input[idx];
                }

                int distinctElements = characters.Distinct().Count();
                if (distinctElements == amountOfDistinctChar)
                {
                    return i;
                }
            }

            throw new Exception();

        }
    }
}