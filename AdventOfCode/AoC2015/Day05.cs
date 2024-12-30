using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal class Day05 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 5, 1);

            var vowels = "aeiou";

            var niceStrings = 0;

            foreach (var item in input)
            {
                if (ContainsPoisonString(item))
                    continue;

                if (!ContainsDoubleLetter(item))
                    continue;

                if (item.Count(c => vowels.Contains(c)) < 3)
                    continue;

                niceStrings++;
            }

            return niceStrings.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 5, 1);

            var niceStrings = 0;

            foreach (var item in input)
            {
                if (!ContainsRepeatPair(item))
                    continue;

                if (!ContainsSandwichLetter(item))
                    continue;

                niceStrings++;
            }

            return niceStrings.ToString();
        }

        private bool ContainsPoisonString(string str)
        {
            if (str.Contains("ab") ||
                str.Contains("cd") ||
                str.Contains("pq") ||
                str.Contains("xy"))
                return true;

            return false;
        }

        private bool ContainsDoubleLetter(string str)
        {
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i+1])
                    return true;
            }
            return false;
        }

        private bool ContainsRepeatPair(string str)
        {
            for (int i = 0; i < str.Length - 3; i++)
            {
                if (str[(i + 2)..].Contains(str.Substring(i, 2)))
                    return true;
            }
            return false;
        }

        private bool ContainsSandwichLetter(string str)
        {
            for (int i = 0; i < str.Length - 2; i++)
            {
                if (str[i] == str[i + 2])
                    return true;
            }
            return false;
        }
    }
}
