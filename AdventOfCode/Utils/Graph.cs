using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    internal class Graph<TName, TValue>(bool isDirected) where TName : notnull
    {
        private readonly Dictionary<TName, AoCNode<TName, TValue>> nodes = [];
        private readonly Dictionary<(AoCNode<TName, TValue>, AoCNode<TName, TValue>), AoCEdge<TName>> edges = [];
        private readonly bool _isDirected = isDirected;

        public bool AddNode(AoCNode<TName, TValue> node)
        { 
            return nodes.TryAdd(node.Name, node);
        }

        public bool AddEdge(AoCEdge<TName> edge)
        {
            return edges.TryAdd((nodes[edge.Source], nodes[edge.Destination]), edge);
        }

        public IEnumerable<AoCNode<TName, TValue>>? FindShortestPathThroughAll(bool doPrint = false, bool returnToStart = false)
        {
            var allPermutations = AoCListAlgorithms.AllPermutations(nodes.Values, returnToStart).Select(x => x.ToArray());

            IEnumerable<AoCNode<TName, TValue>>? shortest = null;
            int shortestLength = int.MaxValue;

            foreach (var path in allPermutations)
            {
                var dist = FindDistance(path);

                if (doPrint)
                {
                    if (dist == null)
                        Console.Write("No path");
                    else
                        Console.Write(dist);

                    Console.Write(" : ");

                    var pathStr = string.Join(" -> ", path.Select(s => s.Name));

                    Console.WriteLine(pathStr);
                }

                if (dist == null)
                    continue;

                if (dist < shortestLength)
                {
                    shortestLength = (int)dist;
                    shortest = path;
                }
            }

            return shortest;
        }

        public int? FindShortestDistanceThroughAll(bool doPrint = false, bool returnToStart = false)
        {
            var shortestPath = FindShortestPathThroughAll(doPrint, returnToStart);

            if (shortestPath == null)
                return null;

            return FindDistance(shortestPath);
        }

        public IEnumerable<AoCNode<TName, TValue>>? FindLongestPathThroughAll(bool doPrint = false, bool returnToStart = false)
        {
            var allPermutations = AoCListAlgorithms.AllPermutations(nodes.Values, returnToStart).Select(x => x.ToArray());

            IEnumerable<AoCNode<TName, TValue>>? longest = null;
            int longestLength = int.MinValue;

            foreach (var path in allPermutations)
            {
                var dist = FindDistance(path);

                if (doPrint)
                {
                    if (dist == null)
                        Console.Write("No path");
                    else
                        Console.Write(dist);

                    Console.Write(" : ");

                    var pathStr = string.Join(" -> ", path.Select(s => s.Name));

                    Console.WriteLine(pathStr);
                }

                if (dist == null)
                    continue;

                if (dist > longestLength)
                {
                    longestLength = (int)dist;
                    longest = path;
                }
            }

            return longest;
        }

        public int? FindLongestDistanceThroughAll(bool doPrint = false, bool returnToStart = false)
        {
            var longestPath = FindLongestPathThroughAll(doPrint, returnToStart);

            if (longestPath == null)
                return null;

            return FindDistance(longestPath);
        }

        public int? FindDistance(IEnumerable<AoCNode<TName, TValue>> path)
        {
            var pathArray = path.ToArray();

            var dist = 0;

            for (int i = 1; i < path.Count(); i++)
            {
                if (edges.ContainsKey((pathArray[i - 1], pathArray[i])))
                    dist += edges[(pathArray[i - 1], pathArray[i])].Weight;
                else if (!_isDirected && edges.ContainsKey((pathArray[i], pathArray[i - 1])))
                    dist += edges[(pathArray[i], pathArray[i - 1])].Weight;
                else
                    return null;
            }

            return dist;
        }
    }

    internal class AoCNode<TName, TValue>(TName name, TValue value)
    {
        private readonly TName _name = name;
        private readonly TValue _value = value;

        public TName Name { get { return _name; } }
        public TValue Value { get { return _value; } }
    }

    internal class AoCEdge<T>(T source, T dest, int weight)
    {
        private readonly T _source = source;
        private readonly T _destination = dest;
        private readonly int _weight = weight;

        public T Source { get { return _source; } }
        public T Destination { get { return _destination; } }
        public int Weight { get { return _weight;} }
    }
}
