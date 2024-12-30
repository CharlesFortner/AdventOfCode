using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day09 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsString(2024, 9, 1);

            //input = "2333133121414131402";

            var expandedDisk = ExpandDiskMap(input);

            while (!IsDiskCompressed(expandedDisk))
            {
                var emptyIndex = expandedDisk.FindIndex(n => n < 0);
                var fileIndex = expandedDisk.FindLastIndex(n => n >= 0);

                expandedDisk[emptyIndex] = expandedDisk[fileIndex];
                expandedDisk[fileIndex] = -1;
            }

            return CalculateCheckSum(expandedDisk).ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsString(2024, 9, 1);

            //input = "2333133121414131402";

            var expandedDisk = ExpandDiskMap(input);

            for (int i = expandedDisk.Max(); i > 0; i--)
            {
                var count = expandedDisk.Count(n => n == i);

                if (count == 0)
                    continue;

                var blockIndex = expandedDisk.IndexOf(i);

                for (int j = 0; j < blockIndex; j++)
                {
                    if (!expandedDisk.GetRange(j, count).All(n => n < 0))
                        continue;

                    expandedDisk.SetRange(i, j, count);
                    expandedDisk.SetRange(-1, blockIndex, count);
                    break;
                }
            }

            return CalculateCheckSum(expandedDisk).ToString();
        }

        private List<int> ExpandDiskMap(string map)
        {
            var disk = new List<int>();
            bool isFile = true;
            int currId = 0;

            foreach (var key in map)
            {
                int keyVal = key - '0';

                for (int i = 0; i < keyVal; i++)
                {
                    if (!isFile)
                    {
                        disk.Add(-1);
                        continue;
                    }

                    disk.Add(currId);
                }

                if (isFile)
                    currId++;

                isFile = !isFile;
            }

            return disk;
        }

        private bool IsDiskCompressed(List<int> disk)
        {
            for (int i = 1; i < disk.Count(); i++)
            {
                if (disk[i] >= 0 && disk[i - 1] < 0)
                    return false;
            }
            return true;
        }

        private long CalculateCheckSum(List<int> disk)
        {
            long sum = 0;

            for (int i = 0; i < disk.Count();  ++i)
            {
                if (disk[i] > 0)
                    sum += i * disk[i];
            }

            return sum;
        }
    }
}
