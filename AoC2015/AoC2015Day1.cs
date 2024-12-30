using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode;

namespace AoC2015
{
    internal class AoC2015Day1
    {
        public static int Part1()
        {
            string input = File.ReadAllText(Path.Combine(AoCConstants.InputDirectory, "2015", "Day 1 Part 1.txt"));

            int floor = 0;

            foreach (char c in input)
            {
                if (c == '(')
                    floor++;
                else if (c == ')')
                    floor--;
            }

            return floor;
        }
    }
}
