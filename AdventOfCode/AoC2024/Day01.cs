using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day01 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 1, 1);

            var leftNums = new List<int>();
            var rightNums = new List<int>();

            var sum = 0;

            foreach (var line in input)
            {
                var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                leftNums.Add(int.Parse(split[0]));
                rightNums.Add(int.Parse(split[1]));
            }

            leftNums.Sort();
            rightNums.Sort();

            for (int i = 0; i < leftNums.Count; i++)
            {
                sum += Math.Abs(leftNums[i] - rightNums[i]);
            }

            return sum.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 1, 1);

            var leftNums = new List<int>();
            var rightNums = new List<int>();

            var sum = 0;

            foreach (var line in input)
            {
                var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                leftNums.Add(int.Parse(split[0]));
                rightNums.Add(int.Parse(split[1]));
            }

            for (int i = 0; i < leftNums.Count; i++)
            {
                sum += leftNums[i] * rightNums.Count(n => n == leftNums[i]);
            }

            return sum.ToString();
        }
    }
}
