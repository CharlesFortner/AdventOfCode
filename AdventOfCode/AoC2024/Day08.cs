using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day08 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 8, 1).ToList();

            //input = "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............".Split("\r\n").ToList();

            var grid = new Grid<char>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }

            var antinodes = new HashSet<(int x, int y)>();

            foreach (char c in GetCharsInGrid(grid))
            {
                var indices = grid.IndicesOf(c).ToArray();

                if (indices.Length <= 1)
                    continue;

                for (int i = 0; i < indices.Length - 1; i++)
                {
                    for (int j = i + 1; j < indices.Length; j++)
                    {
                        var xDist = indices[i].x - indices[j].x;
                        var yDist = indices[i].y - indices[j].y;

                        (int x, int y) antinode1 = (indices[i].x + xDist, indices[i].y + yDist);
                        (int x, int y) antinode2 = (indices[j].x - xDist, indices[j].y - yDist);

                        if (antinode1.x >= 0 &&
                            antinode1.x < input.Count &&
                            antinode1.y >= 0 &&
                            antinode1.y < input[0].Length)
                            antinodes.Add(antinode1);

                        if (antinode2.x >= 0 &&
                            antinode2.x < input.Count &&
                            antinode2.y >= 0 &&
                            antinode2.y < input[0].Length)
                            antinodes.Add(antinode2);
                    }
                }
            }

            return antinodes.Count.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 8, 1).ToList();

            //input = "............\r\n........0...\r\n.....0......\r\n.......0....\r\n....0.......\r\n......A.....\r\n............\r\n............\r\n........A...\r\n.........A..\r\n............\r\n............".Split("\r\n").ToList();

            var grid = new Grid<char>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }

            var antinodes = new HashSet<(int x, int y)>();

            foreach (char c in GetCharsInGrid(grid))
            {
                var indices = grid.IndicesOf(c).ToArray();

                if (indices.Length <= 1)
                    continue;

                for (int i = 0; i < indices.Length - 1; i++)
                {
                    for (int j = i + 1; j < indices.Length; j++)
                    {
                        var offset = GetReducedOffset(indices[i], indices[j]);

                        var currIndex = indices[i];

                        while (IsPointInGrid(currIndex, grid.X, grid.Y))
                        {
                            antinodes.Add(currIndex);
                            currIndex = (currIndex.x + offset.x, currIndex.y + offset.y);
                        }

                        currIndex = indices[i];

                        while (IsPointInGrid(currIndex, grid.X, grid.Y))
                        {
                            antinodes.Add(currIndex);
                            currIndex = (currIndex.x - offset.x, currIndex.y - offset.y);
                        }
                    }
                }
            }

            return antinodes.Count.ToString();
        }

        private List<char> GetCharsInGrid(Grid<char> grid)
        {
            var list = new List<char>();

            for (char i = '0'; i <= '9'; i++)
            {
                if (grid.IndicesOf(i).Count() > 0)
                    list.Add(i);
            }

            for (char i = 'A'; i <= 'Z'; i++)
            {
                if (grid.IndicesOf(i).Count() > 0)
                    list.Add(i);
            }

            for (char i = 'a'; i <= 'z'; i++)
            {
                if (grid.IndicesOf(i).Count() > 0)
                    list.Add(i);
            }

            return list;
        }
        private void PrintGrid(Grid<char> grid)
        {
            for (int i = 0; i < grid.X; i++)
            {
                Console.Write('\n');

                for (int j = 0; j < grid.Y; j++)
                {
                    Console.Write(grid[i, j]);
                }
            }
            Console.Write('\n');
        }
        private (int x, int y) GetReducedOffset((int x, int y) a, (int x, int y) b)
        {
            (int x, int y) diff = (a.x - b.x, a.y - b.y);

            var gcd = AoCMath.GreatestCommonDenominator([diff.x, diff.y]);

            return (diff.x / gcd, diff.y / gcd);
        }
        private bool IsPointInGrid((int x, int y) point, int xMax, int yMax)
        {
            if (point.x >= 0 &&
                point.x < xMax &&
                point.y >= 0 &&
                point.y < yMax)
                return true;
            return false;
        }
    }
}
