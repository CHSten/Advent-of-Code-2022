using Advent_of_Code_2022;
using System.ComponentModel;

namespace Day11
{
    internal class Program
    {
        public class Monkey
        {
            public List<ulong> Items = new List<ulong>();
            public Func<ulong, ulong> Inspect;
            public int TestDivisor;
            public int TrueMonkey;
            public int FalseMonkey;
            public int NumberOfInspections;
        }

        static void Main(string[] args)
        {
            var input = Input.Get();
            var lines = input.Split("\r\n");
            List<Monkey> monkeys = new List<Monkey>();

            monkeys = InitializeMonkeys(lines);

            for (int r = 0; r < 20; r++)
            {
                for (int m = 0; m < monkeys.Count; m++)
                {
                    while (monkeys[m].Items.Count != 0)
                    {
                        ulong item = monkeys[m].Items[0];

                        //Inspect
                        item = monkeys[m].Inspect(item);
                        monkeys[m].NumberOfInspections++;

                        //Relief
                        item = (ulong)Math.Round((double)(item / 3), 0, MidpointRounding.ToZero);

                        //Throw
                        monkeys[m].Items.RemoveAt(0);
                        if (item % (ulong)monkeys[m].TestDivisor == 0)
                            monkeys[monkeys[m].TrueMonkey].Items.Add(item);
                        else
                            monkeys[monkeys[m].FalseMonkey].Items.Add(item);
                    }
                }
            }

            monkeys = monkeys.OrderByDescending(x => x.NumberOfInspections).ToList();
            ulong monkeybusiness = (ulong)(monkeys[0].NumberOfInspections * monkeys[1].NumberOfInspections);

            Console.WriteLine($"The level of monkey business after 20 rounds is:\n" +
                $"{monkeybusiness}");

            monkeys.Clear();
            monkeys = InitializeMonkeys(lines);

            int common_multiple = monkeys[0].TestDivisor;

            for (int i = 1; i < monkeys.Count; i++)
                common_multiple *= monkeys[i].TestDivisor;

            for (int r = 0; r < 10000; r++)
            {
                for (int m = 0; m < monkeys.Count; m++)
                {
                    var monkey = monkeys[m];
                    while (monkey.Items.Count != 0)
                    {
                        ulong item = monkeys[m].Items[0];

                        //Inspect
                        item = monkeys[m].Inspect(item);
                        monkeys[m].NumberOfInspections++;
                        item = item % (ulong)common_multiple;

                        //no Relief

                        //Throw
                        monkeys[m].Items.RemoveAt(0);
                        if (item % (ulong)monkeys[m].TestDivisor == 0)
                            monkeys[monkeys[m].TrueMonkey].Items.Add(item);
                        else
                            monkeys[monkeys[m].FalseMonkey].Items.Add(item);
                    }
                }
            }

            monkeys = monkeys.OrderByDescending(x => x.NumberOfInspections).ToList();
            monkeybusiness = ((ulong)monkeys[0].NumberOfInspections * (ulong)monkeys[1].NumberOfInspections);

            Console.WriteLine($"The level of monkey business after 10000 rounds is:\n" +
                $"{monkeybusiness}");
        }

        private static List<Monkey> InitializeMonkeys(string[] lines)
        {
            Monkey lastMonkey = new Monkey();
            List<Monkey> result = new List<Monkey>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("Monkey"))
                {
                    Monkey monkey = new Monkey();
                    result.Add(monkey);
                    lastMonkey = monkey;
                }

                if (lines[i].StartsWith("  Starting items"))
                {
                    int titleLength = "  Starting items: ".Length;
                    string startingItems = lines[i].Substring(titleLength, lines[i].Length - titleLength);
                    var items = startingItems.Split(", ");

                    for (int j = 0; j < items.Length; j++)
                    {
                        lastMonkey.Items.Add(ulong.Parse(items[j]));
                    }
                }

                if (lines[i].StartsWith("  Operation"))
                {
                    int titleLength = "  Operation: ".Length;
                    string operation = lines[i].Substring(titleLength, lines[i].Length - titleLength);
                    int startIdx = "  Operation: new = old + ".Length;

                    if (operation.Contains("+"))
                    {
                        if (ulong.TryParse(lines[i].Substring(startIdx, lines[i].Length - startIdx), out ulong number))
                            lastMonkey.Inspect = x => x + number;
                        else
                            lastMonkey.Inspect = x => x + x;
                    }
                    else if (operation.Contains("*"))
                    {
                        if (ulong.TryParse(lines[i].Substring(startIdx, lines[i].Length - startIdx), out ulong number))
                            lastMonkey.Inspect = x => x * number;
                        else
                            lastMonkey.Inspect = x => x * x;
                    }
                }

                if (lines[i].StartsWith("  Test"))
                {
                    int startIdx = "  Test: divisible by ".Length;
                    int number = int.Parse(lines[i].Substring(startIdx, lines[i].Length - startIdx));
                    lastMonkey.TestDivisor = (int)number;
                }

                if (lines[i].StartsWith("    If true"))
                    lastMonkey.TrueMonkey = int.Parse(lines[i].Last().ToString());

                if (lines[i].StartsWith("    If false"))
                    lastMonkey.FalseMonkey = int.Parse(lines[i].Last().ToString());
            }

            return result;
        }
    }
}