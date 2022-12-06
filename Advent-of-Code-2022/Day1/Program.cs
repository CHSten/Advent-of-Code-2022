using Advent_of_Code_2022;

namespace Day1
{
    internal class Program
    {
        private class Elf
        {
            public Elf()
            {
                FoodCalories = new List<int>();
            }
            public List<int> FoodCalories { get; set; }
            public int TotalCalories { get; set; }
        }
        static void Main(string[] args)
        {
            string input = Input.Get();
            string[] calories = input.Split("\r\n");
            List<Elf> elves = new List<Elf>();

            elves.Add(new Elf());
            foreach (var calorie in calories)
            {
                if (string.IsNullOrWhiteSpace(calorie))
                    elves.Add(new Elf());
                else
                {
                    int idx = elves.Count - 1;
                    elves[idx].FoodCalories.Add(int.Parse(calorie));
                    elves[idx].TotalCalories += int.Parse(calorie);
                }

            }

            elves.Sort((a,b) => b.TotalCalories.CompareTo(a.TotalCalories));

            int topThreeElves = elves[0].TotalCalories + elves[1].TotalCalories + elves[2].TotalCalories;

            Console.WriteLine($"Elf with the most amount of calories:\n" +
                $"{elves[0].TotalCalories}");

            Console.WriteLine($"Top 3 elves total amount of calories:\n" +
                $"{topThreeElves}");
        }
    }
}