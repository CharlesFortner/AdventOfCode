using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Utils;

namespace AdventOfCode.AoC2015
{
    internal class Day02 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 2, 1);

            var total = 0;

            foreach (var item in input)
            {
                var lengths = item.Split('x').Select(s => int.Parse(s)).ToArray();

                var areas = new List<int>
                {
                    lengths[0] * lengths[1],
                    lengths[1] * lengths[2],
                    lengths[0] * lengths[2]
                };

                var smallestSide = areas.Min();

                total += areas.Sum() * 2;
                total += smallestSide;
            }

            return total.ToString();
        }
        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 2, 1);

            var total = 0;

            foreach (var item in input)
            {
                var lengths = item.Split('x').Select(s => int.Parse(s)).Order().ToArray();

                var bow = lengths[0] * lengths[1] * lengths[2];

                var loop = (lengths[0] + lengths[1]) * 2;

                total += bow;
                total += loop;
            }

            return total.ToString();
        }
    }
}
