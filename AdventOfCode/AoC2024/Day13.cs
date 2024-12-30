using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal partial class Day13 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsString(2024, 13, 1);

            input = "Button A: X+94, Y+34\r\nButton B: X+22, Y+67\r\nPrize: X=8400, Y=5400\r\n\r\nButton A: X+26, Y+66\r\nButton B: X+67, Y+21\r\nPrize: X=12748, Y=12176\r\n\r\nButton A: X+17, Y+86\r\nButton B: X+84, Y+37\r\nPrize: X=7870, Y=6450\r\n\r\nButton A: X+69, Y+23\r\nButton B: X+27, Y+71\r\nPrize: X=18641, Y=10279";

            var matches = ClawMachineParser().Matches(input);

            var sum = 0;

            foreach (Match match in matches.Cast<Match>())
            {
                var coefficients = new Matrix(2);

                coefficients[0, 0] = decimal.Parse(match.Groups[1].Value);
                coefficients[0, 1] = decimal.Parse(match.Groups[2].Value);
                coefficients[1, 0] = decimal.Parse(match.Groups[3].Value);
                coefficients[1, 1] = decimal.Parse(match.Groups[4].Value);
                
                Matrix? inverse;

                if (!coefficients.TryInvert(out inverse))
                    continue;

                var target = new Matrix(2, 1);

                target[0, 0] = decimal.Parse(match.Groups[5].Value);
                target[1, 0] = decimal.Parse(match.Groups[6].Value);

                var result = inverse * target;

                if (result[0,0] > 100 || !decimal.IsInteger(result[0, 0]))
                    continue;
                if (result[1, 0] > 100 || !decimal.IsInteger(result[1, 0]))
                    continue;

                int a = (int)result[0, 0];
                int b = (int)result[1, 0];

                sum += a * 3;
                sum += b;
            }

            return sum.ToString();
        }

        public string Part2()
        {
            throw new NotImplementedException();
        }

        [GeneratedRegex("Button A: X\\+(-?\\d+), Y\\+(-?\\d+)\\r\\nButton B: X\\+(-?\\d+), Y\\+(-?\\d+)\\r\\nPrize: X=(-?\\d+), Y=(-?\\d+)", RegexOptions.Multiline)]
        private static partial Regex ClawMachineParser();
    }
}
