using Advent_of_Code_2022;
using System.ComponentModel;

namespace Day11
{
    internal class Program
    {
        public class Monkey
        {
            public int ID;
            public List<int> StartingItems = new List<int>();
            public Action Operation;
            public Func<bool> Test;
            public int TrueMonkey;
            public int FalseMonkey;
        }

        static void Main(string[] args)
        {
            var input = Input.Get();
        }
    }
}