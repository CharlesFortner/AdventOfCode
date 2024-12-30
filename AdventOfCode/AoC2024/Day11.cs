using AdventOfCode.Utils;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day11 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsString(2024, 11, 1);

            //input = "125 17";

            var stoneList = input.Split(' ').Select(ulong.Parse).ToList();

            for (int i = 0; i < 25; i++)
            {
                Blink(stoneList);
                Console.WriteLine($"Blink {i + 1}: {stoneList.Count,20} Stones");
            }

            return stoneList.Count.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsString(2024, 11, 1);

            //input = "125 17";

            var stoneList = input.Split(' ').Select(ulong.Parse).ToList();

            var cache = new Dictionary<(ulong stone, int blinkNum), ulong>();

            ulong totalStones = CountStones(stoneList, 0, 75, cache);

            return totalStones.ToString();
        }

        private void Blink(List<ulong> stones)
        {
            var numStones = stones.Count;

            for (int i = 0; i < numStones; i++)
            {
                if (stones[i] == 0)
                    stones[i] = 1;

                else if (stones[i].ToString().Length % 2 == 0)
                {
                    var numStr = stones[i].ToString();

                    var newLen = numStr.Length / 2;

                    stones.Add(ulong.Parse(numStr[newLen..]));
                    stones[i] = ulong.Parse(numStr[..newLen]);
                }

                else
                    stones[i] *= 2024;
            }
        }

        private ulong CountStones(List<ulong> stones, int blinkCount, int targetBlinkCount, Dictionary<(ulong stone, int blinkNum), ulong> cache)
        {
            if (blinkCount == targetBlinkCount)
            {
                return (ulong)stones.Count;
            }

            ulong totalStones = 0;
            ulong stoneTotal;

            foreach (var stone in stones)
            {
                if (cache.ContainsKey((stone, blinkCount)))
                {
                    totalStones += cache[(stone, blinkCount)];
                    continue;
                }

                stoneTotal = 0;

                if (stone == 0)
                {
                    stoneTotal += CountStones([1], blinkCount + 1, targetBlinkCount, cache);
                }
                else if (stone.ToString().Length % 2 == 0)
                {
                    var numStr = stone.ToString();
                    var newLen = numStr.Length / 2;

                    stoneTotal += CountStones([ulong.Parse(numStr[newLen..]), ulong.Parse(numStr[..newLen])], blinkCount + 1, targetBlinkCount, cache);
                }
                else
                {
                    stoneTotal += CountStones([stone * 2024], blinkCount + 1, targetBlinkCount, cache);
                }

                cache.Add((stone, blinkCount), stoneTotal);
                totalStones += stoneTotal;
            }

            if (totalStones > 100000000)
            {
                Console.WriteLine($"BlinkCount: {blinkCount,2} Total Stones: {totalStones,15:N0}");
            }

            return totalStones;
        }
    }
}
