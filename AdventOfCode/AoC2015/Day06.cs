using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day06 : AoCDay
    {
        public string Part1()
        {
            var lights = new bool[1000,1000];

            var input = FileLoader.LoadInputFileAsList(2015, 6, 1);

            foreach (var line in input)
            {
                var match = LineParser().Match(line);

                var xStart = int.Parse(match.Groups[2].Value);
                var yStart = int.Parse(match.Groups[3].Value);
                var xEnd = int.Parse(match.Groups[4].Value);
                var yEnd = int.Parse(match.Groups[5].Value);

                for (int i = xStart; i <= xEnd; i++)
                {
                    for (int j = yStart; j <= yEnd; j++)
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "on":
                                lights[i, j] = true;
                                break;
                            case "off":
                                lights[i, j] = false;
                                break;
                            case "toggle":
                                lights[i, j] = !lights[i, j];
                                break;
                        }
                    }
                }
            }

            return lights.Cast<bool>().Count(l => l).ToString();
        }

        public string Part2()
        {
            var lights = new int[1000, 1000];

            var input = FileLoader.LoadInputFileAsList(2015, 6, 1);

            foreach (var line in input)
            {
                var match = LineParser().Match(line);

                var xStart = int.Parse(match.Groups[2].Value);
                var yStart = int.Parse(match.Groups[3].Value);
                var xEnd = int.Parse(match.Groups[4].Value);
                var yEnd = int.Parse(match.Groups[5].Value);

                for (int i = xStart; i <= xEnd; i++)
                {
                    for (int j = yStart; j <= yEnd; j++)
                    {
                        switch (match.Groups[1].Value)
                        {
                            case "on":
                                lights[i, j] += 1;
                                break;
                            case "off":
                                if (lights[i,j] > 0)
                                    lights[i, j] -= 1;
                                break;
                            case "toggle":
                                lights[i, j] += 2;
                                break;
                        }
                    }
                }
            }

            return lights.Cast<int>().Sum().ToString();
        }

        [GeneratedRegex("([a-z]+) (\\d+),(\\d+) through (\\d+),(\\d+)")]
        private static partial Regex LineParser();
    }
}
