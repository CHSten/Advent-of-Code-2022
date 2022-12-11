using Advent_of_Code_2022;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace Day8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Input.Get();
            string[] rows = input.Split("\r\n");
            string treeArray = string.Join("",rows);
            int width = rows[0].Length;
            int height = rows.Length;
            int[,] tallestTreeLeft = new int[width, height],
                tallestTreeRight = new int[width, height],
                tallestTreeUp = new int[width, height],
                tallestTreeDown = new int[width, height];

            for (int yDown = 0, yUp = height-1; yDown < height && yUp >= 0; yDown++, yUp--)
            {
                for (int xLeft = 0, xRight = width-1; xLeft < width && xRight >= 0; xLeft++, xRight--)
                {

                    //Left
                    int idx = yDown * width + xLeft;
                    int tree = int.Parse(treeArray[idx].ToString());

                    if (xLeft - 1 < 0 || tree > tallestTreeLeft[xLeft - 1, yDown])
                        tallestTreeLeft[xLeft, yDown] = tree;
                    else
                        tallestTreeLeft[xLeft, yDown] = tallestTreeLeft[xLeft - 1, yDown];

                    //Right
                    idx = yDown * width + xRight;
                    tree = int.Parse(treeArray[idx].ToString());

                    if (xRight+1 > width-1 || tree > tallestTreeRight[xRight + 1, yDown])
                        tallestTreeRight[xRight, yDown] = tree;
                    else
                        tallestTreeRight[xRight, yDown] = tallestTreeRight[xRight + 1, yDown];

                    //Down
                    idx = yDown * width + xLeft;
                    tree = int.Parse(treeArray[idx].ToString());

                    if (yDown - 1 < 0 || tree > tallestTreeDown[xLeft, yDown - 1])
                        tallestTreeDown[xLeft, yDown] = tree;
                    else
                        tallestTreeDown[xLeft, yDown] = tallestTreeDown[xLeft, yDown - 1];

                    //Up
                    idx = yUp * width + xLeft;
                    tree = int.Parse(treeArray[idx].ToString());

                    if (yUp+1 > height-1 || tree > tallestTreeUp[xLeft, yUp + 1])
                        tallestTreeUp[xLeft, yUp] = tree;
                    else
                        tallestTreeUp[xLeft, yUp] = tallestTreeUp[xLeft, yUp + 1];

                }
            }

            int visibleTrees = 0;
            for (int i = 0; i < treeArray.Length; i++)
            {
                int tree = int.Parse(treeArray[i].ToString());
                int x = i % width;
                int y = i / width;

                if (x == 0 || x == width - 1 ||
                    y == 0 || y == height - 1)
                {
                    visibleTrees++;
                    continue;
                }

                if (tallestTreeLeft[x-1, y] < tree ||
                    tallestTreeRight[x+1, y] < tree ||
                    tallestTreeDown[x, y-1] < tree ||
                    tallestTreeUp[x, y+1] < tree)
                        visibleTrees++;
            }

            Console.WriteLine($"The number of visible trees:\n" +
                $"{visibleTrees}");

            int highestScenicScore = 0;
            for (int i = 0; i < treeArray.Length; i++)
            {
                int treeX = i % width,
                    treeY = i / width;

                if (treeX == 2 && treeY == 3)
                { }

                int tree = int.Parse(treeArray[i].ToString());

                int leftScore = 0,
                    rightScore = 0,
                    downScore = 0,
                    upScore = 0;

                int x = treeX;
                do //Left Score
                {
                    if (x-1 < 0)
                        break;

                    x--;
                    leftScore++;
                } 
                while (int.Parse(treeArray[treeY * width + x].ToString()) < tree);

                x = treeX;
                do //Right Score
                {
                    if (x+1 >= width)
                        break;

                    x++;
                    rightScore++;
                } 
                while (int.Parse(treeArray[treeY * width + x].ToString()) < tree);

                int y = treeY;
                do //Down Score
                {
                    if (y-1 < 0)
                        break;

                    y--;
                    downScore++;
                } 
                while (int.Parse(treeArray[y * width + treeX].ToString()) < tree);

                y = treeY;
                do //Up Score
                {
                    if (y+1 >= height)
                        break;

                    y++;
                    upScore++;
                } 
                while (int.Parse(treeArray[y * width + treeX].ToString()) < tree);

                if (highestScenicScore < leftScore * rightScore * downScore * upScore)
                    highestScenicScore = leftScore * rightScore * downScore * upScore;
            }

            Console.WriteLine($"Highest possible scenic score:\n" +
                $"{highestScenicScore}");

        }
    }
}