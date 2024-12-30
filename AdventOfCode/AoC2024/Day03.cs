using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal partial class Day03 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsString(2024, 3, 1);

            //input = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

            var sum = ParseMulValue(input);

            return sum.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsString(2024, 3, 1);

            //input = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

            var matches = ConditionalMulParser().Matches(input.Replace('\n',' '));

            var sum = 0;

            foreach (Match match in matches.Cast<Match>())
            {
                sum += ParseMulValue(match.Value);
            }

            return sum.ToString();
        }

        private int ParseMulValue(string input)
        {
            var matches = MulParser().Matches(input);

            var sum = 0;

            foreach (Match match in matches.Cast<Match>())
            {
                var num1 = int.Parse(match.Groups[1].Value);
                var num2 = int.Parse(match.Groups[2].Value);
                sum += num1 * num2;
            }

            return sum;
        }


        [GeneratedRegex("mul\\((\\d{1,3}),(\\d{1,3})\\)")]
        private static partial Regex MulParser();

        [GeneratedRegex("(?<=^)(.*?)(?=don't\\(\\))|(?<=do\\(\\))(.*?)(?=don't\\(\\)|$)")]
        private static partial Regex ConditionalMulParser();
    }
}
