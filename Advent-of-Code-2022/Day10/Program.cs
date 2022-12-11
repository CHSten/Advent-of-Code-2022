using Advent_of_Code_2022;

namespace Day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Input.Get();
            string[] commands = input.Split("\r\n");
            int totalCycles = 0;
            int registerX = 1;
            int sumCycleMarks = 0;
            int spriteOffset = 1;
            int xIdx = 0,
                yIdx = 0; 
            string[,] CRTrows = new string[6,40];

            List<int> cycleMarks = new List<int>()
            {
                20, 60, 100, 140, 180, 220
            };

            for (int i = 0; i < commands.Length; i++)
            {
                int cycles = 0;
                int addX = 0;

                if (commands[i].StartsWith("addx"))
                {
                    string[] command = commands[i].Split(" ");
                    addX += int.Parse(command[1]);
                    cycles += 2;
                }
                else if (commands[i].StartsWith("noop"))
                {
                    cycles++;
                }

                for (int c = 0; c < cycles; c++)
                {
                    totalCycles++; //Start cycle

                    if (xIdx <= (registerX + spriteOffset) && //During cylcle
                        xIdx >= (registerX - spriteOffset))
                        CRTrows[yIdx, xIdx] = "#";
                    else
                        CRTrows[yIdx, xIdx] = ".";

                    xIdx++;
                    if (xIdx > CRTrows.GetLength(1) - 1)
                    {
                        xIdx = 0;
                        yIdx++;
                    }

                    if (cycleMarks.Contains(totalCycles))
                        sumCycleMarks += totalCycles * registerX;
                }

                registerX += addX;
            }

            Console.WriteLine($"Sum of all the important signal strengths:\n" +
                $"{sumCycleMarks}");

            Console.WriteLine($"Rendered image:\n");
            for (int y = 0; y < CRTrows.GetLength(0); y++)
            {
                for (int x = 0; x < CRTrows.GetLength(1); x++)
                {
                    Console.Write(CRTrows[y,x]);
                }
                Console.Write("\n");
            }
        }
    }
}