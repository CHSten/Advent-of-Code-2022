using Advent_of_Code_2022;
using System;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Drawing;
using System.Diagnostics;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

namespace Day9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = Input.Get();
            //input = "R 5\r\nU 8\r\nL 8\r\nD 3\r\nR 17\r\nD 10\r\nL 25\r\nU 20";
            var commands = input.Split("\r\n");
            Point headPosition = new Point(0, 0);
            Point tailPositionPt1 = new Point(0, 0);
            List<Point> knotsPt2 = new List<Point>()
            {
                new Point(0,0), //1 (0)
                new Point(0,0), //2 (1)
                new Point(0,0), //3 (2)
                new Point(0,0), //4 (3)
                new Point(0,0), //5 (4)
                new Point(0,0), //6 (5)
                new Point(0,0), //7 (6)
                new Point(0,0), //8 (7)
                new Point(0,0), //9 (8)
            };
            List<Point> visitedPointsPt1 = new List<Point>()
            {
                new Point(0, 0),
            };
            List<Point> visitedPointsPt2 = new List<Point>()
            {
                new Point(0, 0),
            };
            List<Vector2> neighbours = new List<Vector2>()
            {
                new Vector2(1, 0),
                new Vector2(-1, 0),
                new Vector2(0, 1),
                new Vector2(0, -1),
                new Vector2(1, 1),
                new Vector2(-1, -1),
                new Vector2(-1, 1),
                new Vector2(1, -1),
            };

            for (int i = 0; i < commands.Length; i++)
            {
                var command = commands[i].Split(" ");

                for (int c = 0; c < int.Parse(command[1]); c++)
                {
                    if (command[0] == "R")
                        headPosition.X++;
                    else if (command[0] == "L")
                        headPosition.X--;
                    else if (command[0] == "U")
                        headPosition.Y++;
                    else if (command[0] == "D")
                        headPosition.Y--;

                    if (headPosition == tailPositionPt1)
                        continue;

                    Vector2 newPoisition;
                    Vector2 direction = new Vector2(0, 0);
                    Vector2 thisDistance = Vector2.Zero;
                    Vector2? distance = null;

                    foreach (var point in neighbours)
                    {
                        newPoisition = new Vector2(tailPositionPt1.X + point.X, tailPositionPt1.Y + point.Y);

                        thisDistance = new Vector2(newPoisition.X - headPosition.X, newPoisition.Y - headPosition.Y);

                        if (thisDistance == Vector2.Zero)
                            break;

                        if (!distance.HasValue || thisDistance.Length() < distance.Value.Length())
                        {
                            direction = point;
                            distance = thisDistance;
                        }
                    }

                    if (thisDistance == Vector2.Zero)
                        continue;

                    tailPositionPt1.Offset((int)direction.X, (int)direction.Y);

                    if (!visitedPointsPt1.Contains(tailPositionPt1))
                        visitedPointsPt1.Add(tailPositionPt1);
                }

            }

            Console.WriteLine($"the number of positions the tail visited atleast once:\n" +
                $"{visitedPointsPt1.Count}");

            headPosition = new Point(0, 0);
            for (int i = 0; i < commands.Length; i++)
            {
                var command = commands[i].Split(" ");

                for (int c = 0; c < int.Parse(command[1]); c++)
                {
                    if (command[0] == "R")
                        headPosition.X++;
                    else if (command[0] == "L")
                        headPosition.X--;
                    else if (command[0] == "U")
                        headPosition.Y++;
                    else if (command[0] == "D")
                        headPosition.Y--;

                    if (headPosition == knotsPt2[0])
                        continue;

                    Vector2 newPoisition;
                    Vector2 direction = new Vector2(0, 0);
                    Vector2 thisDistance = Vector2.Zero;
                    Vector2? distance = null;
                    foreach (var point in neighbours)
                    {
                        newPoisition = new Vector2(knotsPt2[0].X + point.X, knotsPt2[0].Y + point.Y);

                        thisDistance = new Vector2(newPoisition.X - headPosition.X, newPoisition.Y - headPosition.Y);

                        if (thisDistance == Vector2.Zero)
                            break;

                        if (!distance.HasValue || thisDistance.Length() < distance.Value.Length())
                        {
                            direction = point;
                            distance = thisDistance;
                        }
                    }

                    if (thisDistance == Vector2.Zero)
                        continue;

                    knotsPt2[0] = new Point(knotsPt2[0].X + (int)direction.X, knotsPt2[0].Y + (int)direction.Y);

                    for (int k = 1; k < knotsPt2.Count; k++)
                    {
                        if (knotsPt2[k] == knotsPt2[k - 1])
                            continue;

                        newPoisition = new Vector2(0, 0);
                        direction = new Vector2(0, 0);
                        thisDistance = Vector2.Zero;
                        distance = null;
                        foreach (var point in neighbours)
                        {
                            newPoisition = new Vector2(knotsPt2[k].X + point.X, knotsPt2[k].Y + point.Y);

                            thisDistance = new Vector2(newPoisition.X - knotsPt2[k - 1].X, newPoisition.Y - knotsPt2[k - 1].Y);

                            if (thisDistance == Vector2.Zero)
                                break;

                            if (!distance.HasValue || thisDistance.Length() < distance.Value.Length())
                            {
                                direction = point;
                                distance = thisDistance;
                            }
                        }

                        if (thisDistance == Vector2.Zero)
                            continue;

                        knotsPt2[k] = new Point(knotsPt2[k].X + (int)direction.X, knotsPt2[k].Y + (int)direction.Y);
                    }

                    if (!visitedPointsPt2.Contains(knotsPt2[knotsPt2.Count-1]))
                        visitedPointsPt2.Add(knotsPt2[knotsPt2.Count - 1]);
                }
            }

            Console.WriteLine($"the number of positions the tail 9 visited atleast once:\n" +
                $"{visitedPointsPt2.Count}");

        }
    }
}