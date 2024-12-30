using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day13 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 13, 1);

            var happinessChart = new Dictionary<(string, string), int>();

            var names = new List<string>();

            var graph = new Graph<string, string>(false);

            foreach (var line in input)
            {
                var match = HappinessParser().Match(line);

                int happiness = int.Parse(match.Groups[3].Value);

                if (match.Groups[2].Value == "lose")
                    happiness = -happiness;

                var name1 = match.Groups[1].Value;
                var name2 = match.Groups[4].Value;

                happinessChart.Add((name1, name2), happiness);

                names.Add(name1);
                names.Add(name2);

                graph.AddNode(new AoCNode<string, string>(name1, name1));
                graph.AddNode(new AoCNode<string, string>(name2, name2));
            }

            names = names.Distinct().ToList();

            for (int i = 0; i < names.Count - 1; i++)
            {
                for (int j = i + 1; j < names.Count; j++)
                {
                    var totalHappiness = happinessChart[(names[i], names[j])] + happinessChart[(names[j], names[i])];

                    graph.AddEdge(new AoCEdge<string>(names[i], names[j], totalHappiness));
                }
            }

            var maxHappiness = graph.FindLongestDistanceThroughAll(false, true);

            if (maxHappiness == null)
                return "No valid arrangement found";

            return ((int)maxHappiness).ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 13, 1);

            var happinessChart = new Dictionary<(string, string), int>();

            var names = new List<string>();

            var graph = new Graph<string, string>(false);

            foreach (var line in input)
            {
                var match = HappinessParser().Match(line);

                int happiness = int.Parse(match.Groups[3].Value);

                if (match.Groups[2].Value == "lose")
                    happiness = -happiness;

                var name1 = match.Groups[1].Value;
                var name2 = match.Groups[4].Value;

                happinessChart.Add((name1, name2), happiness);

                names.Add(name1);
                names.Add(name2);

                graph.AddNode(new AoCNode<string, string>(name1, name1));
                graph.AddNode(new AoCNode<string, string>(name2, name2));
            }

            graph.AddNode(new AoCNode<string, string>("Me", "Me"));

            names = names.Distinct().ToList();

            for (int i = 0; i < names.Count - 1; i++)
            {
                for (int j = i + 1; j < names.Count; j++)
                {
                    var totalHappiness = happinessChart[(names[i], names[j])] + happinessChart[(names[j], names[i])];

                    graph.AddEdge(new AoCEdge<string>(names[i], names[j], totalHappiness));
                }
            }

            foreach (var name in names)
            {
                graph.AddEdge(new AoCEdge<string>(name, "Me", 0));
            }

            var maxHappiness = graph.FindLongestDistanceThroughAll(false, true);

            if (maxHappiness == null)
                return "No valid arrangement found";

            return ((int)maxHappiness).ToString();
        }

        [GeneratedRegex("([A-Z]\\w+).*(gain|lose) (\\d+).*([A-Z]\\w+)")]
        private static partial Regex HappinessParser();
    }
}
