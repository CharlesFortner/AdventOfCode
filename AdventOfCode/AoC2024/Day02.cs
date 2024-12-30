using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day02 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 2, 1);

            var numSafe = 0;

            foreach (var line in input)
            {
                var nums = line.Split(' ').Select(c => int.Parse(c)).ToArray();

                if (IsReportSafe(nums))
                {
                    numSafe++;
                }
            }

            return numSafe.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 2, 1);

            var numSafe = 0;

            foreach (var line in input)
            {
                var nums = line.Split(' ').Select(c => int.Parse(c)).ToArray();

                if (IsReportSafe(nums))
                {
                    numSafe++;
                    continue;
                }

                if (IsReportSafeDamped(nums))
                {
                    numSafe++;
                    continue;
                }
            }

            return numSafe.ToString();
        }

        private bool IsReportSafe(int[] input)
        {
            bool safe = true;
            bool increase = (input[1] - input[0]) > 0;

            for (int i = 1; i < input.Count(); i++)
            {
                var diff = Math.Abs(input[i] - input[i - 1]);

                if (diff <= 0 || diff >= 4)
                {
                    safe = false;
                    break;
                }

                if (increase && (input[i] - input[i - 1]) <= 0)
                {
                    safe = false;
                    break;
                }
                else if (!increase && (input[i] - input[i - 1]) >= 0)
                {
                    safe = false;
                    break;
                }
            }

            if (safe)
            {
                return true;
            }
            return false;
        }

        private bool IsReportSafeDamped(int[] input)
        {

            for (int j = 0; j < input.Count(); j++)
            {
                var shortInput = input.ToList();
                shortInput.RemoveAt(j);

                bool safe = true;
                bool increase = (shortInput[1] - shortInput[0]) > 0;

                for (int i = 1; i < shortInput.Count(); i++)
                {
                    var diff = Math.Abs(shortInput[i] - shortInput[i - 1]);

                    if (diff <= 0 || diff >= 4)
                    {
                        safe = false;
                        break;
                    }

                    if (increase && (shortInput[i] - shortInput[i - 1]) <= 0)
                    {
                        safe = false;
                        break;
                    }
                    else if (!increase && (shortInput[i] - shortInput[i - 1]) >= 0)
                    {
                        safe = false;
                        break;
                    }
                }

                if (safe)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
