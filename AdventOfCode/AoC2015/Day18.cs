using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal class Day18 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 18, 1).ToList();

            var grid = new Grid<bool>(input.Count, input[0].Length, false);

            for (int i = 0; i < input.Count; i++)
            {
                var row = input[i];

                for (int j = 0; j < row.Length; j++)
                {
                    grid[i, j] = row[j] == '#';
                }
            }

            for (int i = 0; i < 100; i++)
            {
                grid = DoStep(grid);
            }

            var numOn = grid.Cast<bool>().Count(x => x);

            return numOn.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 18, 1).ToList();

            var grid = new Grid<bool>(input.Count, input[0].Length, false);

            for (int i = 0; i < input.Count; i++)
            {
                var row = input[i];

                for (int j = 0; j < row.Length; j++)
                {
                    grid[i, j] = row[j] == '#';
                }
            }

            for (int i = 0; i < 100; i++)
            {
                grid = DoStep(grid);
                SetCorners(ref grid);
            }

            var numOn = grid.Cast<bool>().Count(x => x);

            return numOn.ToString();
        }

        public static Grid<bool> DoStep(Grid<bool> grid)
        {
            Grid<bool> result = grid.Clone();

            for (int i = 0; i < grid.Data.GetLength(0); i++)
            {
                for (int j = 0; j < grid.Data.GetLength(1); j++)
                {
                    var onNeighbors = grid.GetNeighbors(i, j).Where(x => x).Count();

                    if (grid[i, j] && onNeighbors != 2 && onNeighbors != 3)
                        result[i, j] = false;

                    else if (!grid[i, j] && onNeighbors == 3)
                        result[i, j] = true;
                }
            }

            return result;
        }

        public static void SetCorners(ref Grid<bool> grid)
        {
            var lengthX = grid.Data.GetLength(0) - 1;
            var lengthY = grid.Data.GetLength(1) - 1;

            grid[0, 0] = true;
            grid[0, lengthY] = true;
            grid[lengthX, lengthY] = true;
            grid[lengthX, 0] = true;
        }
    }
}
