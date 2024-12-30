using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day16 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 16, 1);

            Dictionary<int, Dictionary<string, int>> auntSues = [];

            foreach (var line in input)
            {
                var matchGroups = SueParser().Match(line).Groups;

                var sueNum = int.Parse(matchGroups[1].Value);

                auntSues.Add(sueNum, []);

                auntSues[sueNum].Add(matchGroups[2].Value, int.Parse(matchGroups[3].Value));
                auntSues[sueNum].Add(matchGroups[4].Value, int.Parse(matchGroups[5].Value));
                auntSues[sueNum].Add(matchGroups[6].Value, int.Parse(matchGroups[7].Value));
            }

            var correctSue = auntSues.First(s => MatchSue(s.Value));

            return correctSue.Key.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 16, 1);

            Dictionary<int, Dictionary<string, int>> auntSues = [];

            foreach (var line in input)
            {
                var matchGroups = SueParser().Match(line).Groups;

                var sueNum = int.Parse(matchGroups[1].Value);

                auntSues.Add(sueNum, []);

                auntSues[sueNum].Add(matchGroups[2].Value, int.Parse(matchGroups[3].Value));
                auntSues[sueNum].Add(matchGroups[4].Value, int.Parse(matchGroups[5].Value));
                auntSues[sueNum].Add(matchGroups[6].Value, int.Parse(matchGroups[7].Value));
            }

            var correctSue = auntSues.First(s => MatchSueUpdated(s.Value));

            return correctSue.Key.ToString();
        }

        private bool MatchSue(Dictionary<string, int> sue)
        {
            if (sue.TryGetValue("children", out int value) && value != 3)
                return false;

            if (sue.TryGetValue("cats", out value) && value != 7)
                return false;

            if (sue.TryGetValue("samoyeds", out value) && value != 2)
                return false;

            if (sue.TryGetValue("pomeranians", out value) && value != 3)
                return false;

            if (sue.TryGetValue("akitas", out value) && value != 0)
                return false;

            if (sue.TryGetValue("vizslas", out value) && value != 0)
                return false;

            if (sue.TryGetValue("goldfish", out value) && value != 5)
                return false;

            if (sue.TryGetValue("trees", out value) && value != 3)
                return false;

            if (sue.TryGetValue("cars", out value) && value != 2)
                return false;

            if (sue.TryGetValue("perfumes", out value) && value != 1)
                return false;

            return true;
        }

        private bool MatchSueUpdated(Dictionary<string, int> sue)
        {
            if (sue.TryGetValue("children", out int value) && value != 3)
                return false;

            if (sue.TryGetValue("cats", out value) && value <= 7)
                return false;

            if (sue.TryGetValue("samoyeds", out value) && value != 2)
                return false;

            if (sue.TryGetValue("pomeranians", out value) && value >= 3)
                return false;

            if (sue.TryGetValue("akitas", out value) && value != 0)
                return false;

            if (sue.TryGetValue("vizslas", out value) && value != 0)
                return false;

            if (sue.TryGetValue("goldfish", out value) && value >= 5)
                return false;

            if (sue.TryGetValue("trees", out value) && value <= 3)
                return false;

            if (sue.TryGetValue("cars", out value) && value != 2)
                return false;

            if (sue.TryGetValue("perfumes", out value) && value != 1)
                return false;

            return true;
        }

        [GeneratedRegex("Sue (\\d+): (\\w+): (\\d+), (\\w+): (\\d+), (\\w+): (\\d+)")]
        private static partial Regex SueParser();
    }
}
