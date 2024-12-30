using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day06 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 6, 1).ToList();

            //input = new List<string>() { "....#.....", ".........#", "..........", "..#.......", ".......#..", "..........", ".#..^.....", "........#.", "#.........", "......#..." };

            var grid = new Grid<char>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }

            var guardPosition = grid.IndexOf('^');

            bool guardExited = false;

            while (!guardExited)
            {
                if (guardPosition.x == 0)
                {
                    grid[guardPosition.x, guardPosition.y] = 'X';
                    guardExited = true;
                }
                else if (grid[guardPosition.x - 1, guardPosition.y] == '#')
                {
                    grid.RotateGrid(false);
                    guardPosition = grid.IndexOf('^');
                }
                else
                {
                    grid[guardPosition.x, guardPosition.y] = 'X';
                    grid[guardPosition.x - 1, guardPosition.y] = '^';
                    guardPosition = (guardPosition.x - 1, guardPosition.y);
                }
            }

            return grid.Count('X').ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 6, 1).ToList();

            //input = new List<string>() { "....#.....", ".........#", "..........", "..#.......", ".......#..", "..........", ".#..^.....", "........#.", "#.........", "......#..." };

            var grid = new Grid<char>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j];
                }
            }

            var originGrid = grid.Clone();

            var guardPosition = grid.IndexOf('^');
            var guardOrigin = grid.IndexOf('^');

            bool guardExited = false;

            while (!guardExited)
            {
                if (guardPosition.x == 0)
                {
                    grid[guardPosition.x, guardPosition.y] = 'X';
                    guardExited = true;
                }
                else if (grid[guardPosition.x - 1, guardPosition.y] == '#')
                {
                    grid.RotateGrid(false);
                    originGrid.RotateGrid(false);
                    guardPosition = grid.IndexOf('^');
                }
                else
                {
                    grid[guardPosition.x, guardPosition.y] = 'X';
                    grid[guardPosition.x - 1, guardPosition.y] = '^';
                    guardPosition = (guardPosition.x - 1, guardPosition.y);
                }
            }

            while (!originGrid.IndexOf('^').Equals(guardOrigin))
            {
                grid.RotateGrid(false);
                originGrid.RotateGrid(false);
            }

            var validPositions = 0;

            for (int i = 0; i < grid.X; i++)
            {
                for (int j = 0; j < grid.Y; j++)
                {
                    if (grid[i, j] != 'X')
                        continue;

                    if (originGrid[i, j] == '^')
                        continue;

                    var testGrid = originGrid.Clone();
                    testGrid[i, j] = '#';

                    var loop = TestGrid(testGrid);

                    if (loop)
                        validPositions++;
                }
            }

            return validPositions.ToString();
        }

        private bool TestGrid(Grid<char> grid)
        {
            var guardPosition = grid.IndexOf('^');

            bool guardExited = false;

            int steps = 0;

            while (!guardExited && steps < 20000)
            {
                if (guardPosition.x == 0)
                {
                    grid[guardPosition.x, guardPosition.y] = 'X';
                    guardExited = true;
                }
                else if (grid[guardPosition.x - 1, guardPosition.y] == '#')
                {
                    grid.RotateGrid(false);
                    guardPosition = grid.IndexOf('^');
                }
                else
                {
                    grid[guardPosition.x, guardPosition.y] = 'X';
                    grid[guardPosition.x - 1, guardPosition.y] = '^';
                    guardPosition = (guardPosition.x - 1, guardPosition.y);
                    steps++;
                }
            }

            return !guardExited;
        }



        private void PrintGrid(Grid<char> grid)
        {
            for(int i = 0; i < grid.X; i++)
            {
                Console.Write('\n');

                for (int j = 0; j < grid.Y; j++)
                {
                    Console.Write(grid[i, j]);
                }
            }
            Console.Write('\n');
        }
    }
}
