using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2016
{
    internal class Day01 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsString(2016, 1, 1);

            var split = input.Split(",").Select(s => s.Trim());

            int ns = 0;
            int ew = 0;

            var direction = Direction.North;

            foreach (var instruction in split)
            {
                direction = instruction[0] == 'R' ? TurnRight(direction) : TurnLeft(direction);

                switch (direction)
                {
                    case Direction.North:
                        ns += int.Parse(instruction.Substring(1));
                        break;
                    case Direction.South:
                        ns -= int.Parse(instruction.Substring(1));
                        break;
                    case Direction.East:
                        ew += int.Parse(instruction.Substring(1));
                        break;
                    case Direction.West:
                        ew -= int.Parse(instruction.Substring(1));
                        break;
                }
            }

            return (Math.Abs(ns) + Math.Abs(ew)).ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsString(2016, 1, 1);

            var split = input.Split(",").Select(s => s.Trim());

            int ns = 0;
            int ew = 0;

            var direction = Direction.North;

            var visited = new HashSet<(int ns, int ew)>();

            foreach (var instruction in split)
            {
                direction = instruction[0] == 'R' ? TurnRight(direction) : TurnLeft(direction);

                int travel = int.Parse(instruction.Substring(1));

                for (int i = 0; i < travel; i++)
                {
                    switch (direction)
                    {
                        case Direction.North:
                            ns += 1;
                            break;
                        case Direction.South:
                            ns -= 1;
                            break;
                        case Direction.East:
                            ew += 1;
                            break;
                        case Direction.West:
                            ew -= 1;
                            break;
                    }

                    if (!visited.Add((ns, ew)))
                        return (Math.Abs(ns) + Math.Abs(ew)).ToString();
                }
            }

            return "-1";
        }

        private Direction TurnLeft(Direction current)
        {
            if (current == Direction.North)
                return Direction.West;

            return current - 1;
        }

        private Direction TurnRight(Direction current)
        {
            if (current == Direction.West)
                return Direction.North;

            return current + 1;
        }
    }

    internal enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

}
