using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal class Day08 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 8, 1);

            var codeCount = 0;
            var charCount = 0;

            foreach (var line in input)
            {
                var length = line.Length;

                var unescaped = Regex.Unescape(line);

                charCount += unescaped.Length - 2;
                codeCount += length;
            }

            return (codeCount - charCount).ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 8, 1);

            var codeCount = 0;
            var charCount = 0;

            foreach (var line in input)
            {
                var escaped = Regex.Escape(line);

                Console.WriteLine(line);
                Console.WriteLine(escaped);
                Console.WriteLine();

                codeCount += escaped.Length + 2 + escaped.Count(c => c == '\"');
                charCount += line.Length;
            }

            return (codeCount - charCount).ToString();
        }
    }
}
