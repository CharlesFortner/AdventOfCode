using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day09 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 9, 1);

            var graph = new Graph<string, string>(false);

            foreach (var line in input)
            {
                var match = EdgeParser().Match(line);

                var source = new AoCNode<string, string>(match.Groups[1].Value, match.Groups[1].Value);
                var dest = new AoCNode<string, string>(match.Groups[2].Value, match.Groups[2].Value);

                graph.AddNode(source);
                graph.AddNode(dest);

                var edge = new AoCEdge<string>(source.Name, dest.Name, int.Parse(match.Groups[3].Value));

                graph.AddEdge(edge);
            }

            var shortestPath = graph.FindShortestDistanceThroughAll(true);

            if (shortestPath == null)
                return "No valid path found";

            return ((int)shortestPath).ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 9, 1);

            var graph = new Graph<string, string>(false);

            foreach (var line in input)
            {
                var match = EdgeParser().Match(line);

                var source = new AoCNode<string, string>(match.Groups[1].Value, match.Groups[1].Value);
                var dest = new AoCNode<string, string>(match.Groups[2].Value, match.Groups[2].Value);

                graph.AddNode(source);
                graph.AddNode(dest);

                var edge = new AoCEdge<string>(source.Name, dest.Name, int.Parse(match.Groups[3].Value));

                graph.AddEdge(edge);
            }

            var longestPath = graph.FindLongestDistanceThroughAll(true);

            if (longestPath == null)
                return "No valid path found";

            return ((int)longestPath).ToString();
        }

        [GeneratedRegex("(\\w+) to (\\w+) = (\\d+)")]
        private static partial Regex EdgeParser();
    }
}
