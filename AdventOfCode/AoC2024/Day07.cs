using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day07 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 7, 1);

            //input = "190: 10 19\r\n3267: 81 40 27\r\n83: 17 5\r\n156: 15 6\r\n7290: 6 8 6 15\r\n161011: 16 10 13\r\n192: 17 8 14\r\n21037: 9 7 18 13\r\n292: 11 6 16 20".Split("\r\n");

            ulong sum = 0;

            foreach (var line in input)
            {
                var split = line.Split(' ');

                var target = ulong.Parse(split[0].Trim(':'));

                var numberList = split[1..].Select(ulong.Parse).ToArray();

                if (TestOperators(target, numberList))
                {
                    sum += target;
                }
            }

            return sum.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 7, 1);

            //input = "190: 10 19\r\n3267: 81 40 27\r\n83: 17 5\r\n156: 15 6\r\n7290: 6 8 6 15\r\n161011: 16 10 13\r\n192: 17 8 14\r\n21037: 9 7 18 13\r\n292: 11 6 16 20".Split("\r\n");

            ulong sum = 0;

            foreach (var line in input)
            {
                var split = line.Split(' ');

                var target = ulong.Parse(split[0].Trim(':'));

                var numberList = split[1..].Select(ulong.Parse).ToArray();

                if (TestOperators(target, numberList, true))
                {
                    sum += target;
                }
            }

            return sum.ToString();
        }

        private bool TestOperators(ulong target, ulong[] numbers, bool allowConcat = false)
        {
            if (numbers.Length == 2)
            {
                if (numbers[0] + numbers[1] == target)
                    return true;
                if (numbers[0] * numbers[1] == target)
                    return true;
                if (allowConcat && Concat(numbers[0], numbers[1]) == target)
                    return true;
                return false;
            }

            var newNums = numbers[1..];
            newNums[0] = numbers[0] + numbers[1];

            if (newNums[0] <= target && TestOperators(target, newNums, allowConcat))
                return true;

            newNums[0] = numbers[0] * numbers[1];

            if (newNums[0] <= target && TestOperators(target, newNums, allowConcat))
                return true;

            if (!allowConcat)
                return false;

            newNums[0] = Concat(numbers[0], numbers[1]);

            if (newNums[0] <= target && TestOperators(target, newNums, allowConcat))
                return true;

            return false;
        }

        private ulong Concat(ulong a, ulong b)
        {
            return ulong.Parse(a.ToString() + b.ToString());
        }
    }
}
