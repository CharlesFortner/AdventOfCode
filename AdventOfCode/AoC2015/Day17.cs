using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal class Day17 : AoCDay
    {
        private readonly int _totalVolume = 150;
        List<List<int>> _containerSets = [];

        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 17, 1);

            var containers = new List<int>();

            foreach (var line in input)
            {
                containers.Add(int.Parse(line));
            }

            FillContainers([], containers);

            return _containerSets.Count.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 17, 1);

            var containers = new List<int>();

            foreach (var line in input)
            {
                containers.Add(int.Parse(line));
            }

            FillContainers([], containers);

            var minContainers = _containerSets.Select(s => s.Count).Min();

            return _containerSets.Count(s => s.Count == minContainers).ToString();
        }

        private void FillContainers(List<int> used, List<int> empty)
        {
            var remainingVolume = _totalVolume - used.Sum();

            if (remainingVolume == 0)
            {
                _containerSets.Add(used);
                return;
            }
            if (remainingVolume < 0)
            {
                return;
            }
            
            var newEmpty = new List<int>(empty);

            foreach (var container in empty)
            {
                var newUsed = new List<int>(used);

                newEmpty.Remove(container);
                newUsed.Add(container);

                FillContainers(newUsed, newEmpty);
            }

        }
    }
}
