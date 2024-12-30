using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal class Day03 : AoCDay
    {
        string AoCDay.Part1()
        {
            string input = FileLoader.LoadInputFileAsString(2015, 3, 1);

            var grid = new Dictionary<(int x, int y), int>();
            var x = 0;
            var y = 0;

            grid.Add((x, y), 1);

            foreach (var move in input)
            {
                switch(move)
                {
                    case '<':
                        y--;
                        break;
                    case '>':
                        y++;
                        break;
                    case 'v':
                        x--;
                        break;
                    case '^':
                        x++;
                        break;
                }
                if (grid.ContainsKey((x, y)))
                    grid[(x, y)] += 1;
                else
                    grid.Add((x, y), 1);
            }

            var count = grid.Count;

            return count.ToString();
        }

        string AoCDay.Part2()
        {
            string input = FileLoader.LoadInputFileAsString(2015, 3, 1);

            var grid = new Dictionary<(int x, int y), int>();
            var santaX = 0;
            var santaY = 0;
            var roboX = 0;
            var roboY = 0;

            grid.Add((santaX, santaY), 1);
            grid[(roboX, roboY)] += 1;

            for (int i = 0; i < input.Length; i++)
            {
                // Santa move
                var move = input[i];
                switch (move)
                {
                    case '<':
                        santaY--;
                        break;
                    case '>':
                        santaY++;
                        break;
                    case 'v':
                        santaX--;
                        break;
                    case '^':
                        santaX++;
                        break;
                }
                if (grid.ContainsKey((santaX, santaY)))
                    grid[(santaX, santaY)] += 1;
                else
                    grid.Add((santaX, santaY), 1);

                i++;

                // Robot move
                move = input[i];
                switch (move)
                {
                    case '<':
                        roboY--;
                        break;
                    case '>':
                        roboY++;
                        break;
                    case 'v':
                        roboX--;
                        break;
                    case '^':
                        roboX++;
                        break;
                }
                if (grid.ContainsKey((roboX, roboY)))
                    grid[(roboX, roboY)] += 1;
                else
                    grid.Add((roboX, roboY), 1);
            }

            var count = grid.Count;

            return count.ToString();
        }
    }
}
