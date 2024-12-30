using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Utils;

namespace AdventOfCode.AoC2015
{
    internal class Day01 : AoCDay
    {
        public string Part1()
        {
            string input = FileLoader.LoadInputFileAsString(2015, 1, 1);

            int floor = 0;

            foreach (char c in input)
            {
                if (c == '(')
                    floor++;
                else if (c == ')')
                    floor--;
            }

            return floor.ToString();
        }

        public string Part2()
        {
            string input = FileLoader.LoadInputFileAsString(2015, 1, 2);

            int floor = 0;
            int position = 0;

            foreach (char c in input)
            {
                position++;

                if (c == '(')
                    floor++;
                else if (c == ')')
                    floor--;

                if (floor < 0)
                    return position.ToString();
            }

            return position.ToString();
        }
    }
}
