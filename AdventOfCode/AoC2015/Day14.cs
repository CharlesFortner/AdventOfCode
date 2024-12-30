using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day14 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 14, 1);

            var reindeer = new List<Reindeer>();

            foreach (var line in input)
            {
                var match = ReindeerParser().Match(line);

                reindeer.Add(new Reindeer(match.Groups[1].Value, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)));
            }

            for (int i = 0; i < 2503; i++)
            {
                foreach (var deer in reindeer)
                    deer.ElapseTime();
            }

            var winner = reindeer.OrderByDescending(r => r.Distance).First();

            return winner.Distance.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 14, 1);

            var reindeer = new List<Reindeer>();

            foreach (var line in input)
            {
                var match = ReindeerParser().Match(line);

                reindeer.Add(new Reindeer(match.Groups[1].Value, int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)));
            }

            for (int i = 0; i < 2503; i++)
            {
                foreach (var deer in reindeer)
                    deer.ElapseTime();

                var maxDistance = reindeer.Select(r => r.Distance).Max();

                foreach (var deer in reindeer.Where(r => r.Distance == maxDistance))
                    deer.Score += 1;
            }

            var winner = reindeer.OrderByDescending(r => r.Score).First();

            return winner.Score.ToString();
        }

        [GeneratedRegex("([A-Z]\\w+).* (\\d+).* (\\d+).* (\\d+)")]
        private static partial Regex ReindeerParser();
    }

    internal class Reindeer(string name, int speed, int endurance, int recovery)
    {
        public string Name { get; } = name;
        public int Speed { get; } = speed;
        public int Endurance { get; } = endurance;
        public int Recovery { get; } = recovery;
        public int Distance { get; set; } = 0;
        public int Score { get; set; } = 0;

        private bool _resting = true;

        private int _remainingTime = 0;

        public void ElapseTime()
        {
            if (_remainingTime == 0)
            {
                _resting = !_resting;
                _remainingTime = _resting ? Recovery : Endurance;
            }

            if (!_resting)
            {
                Distance += Speed * 1;
            }

            _remainingTime -= 1;
        }
    }
}
