using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day10 : AoCDay
    {
        public string Part1()
        {
            var input = "3113322113";

            for (int i = 0; i < 40; i++)
            {
                input = LookAndSay(input);
                Console.WriteLine(input.Length);
            }

            return input.Length.ToString();
        }

        public string Part2()
        {
            var input = "3113322113";

            for (int i = 0; i < 50; i++)
            {
                input = LookAndSay(input);
                Console.WriteLine(input.Length);
            }

            return input.Length.ToString();
        }

        private string LookAndSay(string input)
        {
            var matches = DigitGroupParser().Matches(input);

            var output = new StringBuilder();

            foreach (Match match in matches)
            {
                var value = match.Groups[0].Value[0];
                var count = match.Groups[0].Value.Length;

                output.Append(count);
                output.Append(value);
            }

            return output.ToString();
        }

        [GeneratedRegex("(\\d)\\1*")]
        private static partial Regex DigitGroupParser();
    }
}
