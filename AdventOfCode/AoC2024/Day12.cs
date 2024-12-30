using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day12 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 12, 1).ToList();

            //input = ["RRRRIICCFF", "RRRRIICCCF", "VVRRRCCFFF", "VVRCCCJFFF", "VVVVCJJCFE", "VVIVCCJJEE", "VVIIICJJEE", "MIIIIIJJEE", "MIIISIJEEE", "MMMISSJEEE"];

            var grid = new Grid<(char val, int region)>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = (input[i][j], 0);
                }
            }

            int nextRegion = 1;
            var nextNode = grid.IndexOf(IsRegionZero);

            long fencePrice = 0;

            while (nextNode.x is not -1)
            {
                fencePrice += SetNewRegion(grid, nextNode, nextRegion++);

                nextNode = grid.IndexOf(IsRegionZero);
            }

            return fencePrice.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 12, 1).ToList();

            //input = ["RRRRIICCFF", "RRRRIICCCF", "VVRRRCCFFF", "VVRCCCJFFF", "VVVVCJJCFE", "VVIVCCJJEE", "VVIIICJJEE", "MIIIIIJJEE", "MIIISIJEEE", "MMMISSJEEE"];

            var grid = new Grid<(char val, int region)>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = (input[i][j], 0);
                }
            }

            int nextRegion = 1;
            var nextNode = grid.IndexOf(IsRegionZero);

            long fencePrice = 0;

            while (nextNode.x is not -1)
            {
                fencePrice += SetNewRegion(grid, nextNode, nextRegion++, false);

                nextNode = grid.IndexOf(IsRegionZero);
            }

            return fencePrice.ToString();
        }

        private static bool IsRegionZero((char val, int region) plot) => plot.region is 0;

        private static int SetNewRegion(Grid<(char val, int region)> grid, (int x, int y) firstNode, int regionNumber, bool usePerimeter = true)
        {
            var uncheckedNodes = new Queue<(int x, int y)>();
            var regionNodes = new HashSet<(int x, int y)>();

            uncheckedNodes.Enqueue(firstNode);
            var regionValue = grid[firstNode.x, firstNode.y].val;

            while (uncheckedNodes.Count > 0)
            {
                var checkNode = uncheckedNodes.Dequeue();

                if (!regionNodes.Add(checkNode))
                    continue;

                SetRegionNumber(grid, checkNode, regionNumber);

                if (grid.TryGetNeighborLocation(checkNode, GridDirection.North, n => n.val == regionValue && n.region is 0, out var neighborLocation))
                {
                    uncheckedNodes.Enqueue(neighborLocation);
                }
                if (grid.TryGetNeighborLocation(checkNode, GridDirection.East, n => n.val == regionValue && n.region is 0, out neighborLocation))
                {
                    uncheckedNodes.Enqueue(neighborLocation);
                }
                if (grid.TryGetNeighborLocation(checkNode, GridDirection.South, n => n.val == regionValue && n.region is 0, out neighborLocation))
                {
                    uncheckedNodes.Enqueue(neighborLocation);
                }
                if (grid.TryGetNeighborLocation(checkNode, GridDirection.West, n => n.val == regionValue && n.region is 0, out neighborLocation))
                {
                    uncheckedNodes.Enqueue(neighborLocation);
                }
            }
            
            if (usePerimeter)
                return GetPerimeterCost(grid, regionNodes, (regionValue, regionNumber));

            return GetSidesCost(grid, regionNodes, (regionValue, regionNumber));
        }

        private static int GetPerimeterCost(Grid<(char val, int region)> grid, IEnumerable<(int x, int y)> nodes, (char val, int region) regionDef)
        {
            int area = nodes.Count();
            int perimeter = 0;

            foreach (var node in nodes)
            {
                if (!grid.CheckNeighbor(node, GridDirection.North, n => n == regionDef))
                {
                    perimeter++;
                }
                if (!grid.CheckNeighbor(node, GridDirection.East, n => n == regionDef))
                {
                    perimeter++;
                }
                if (!grid.CheckNeighbor(node, GridDirection.South, n => n == regionDef))
                {
                    perimeter++;
                }
                if (!grid.CheckNeighbor(node, GridDirection.West, n => n == regionDef))
                {
                    perimeter++;
                }
            }

            return area * perimeter;
        }

        private static int GetSidesCost(Grid<(char val, int region)> grid, IEnumerable<(int x, int y)> nodes, (char val, int region) regionDef)
        {
            int area = nodes.Count();
            int sides = 0;

            var edges = nodes.Where(l => !grid.CheckNeighbor(l, GridDirection.North, n => n == regionDef)).ToList();

            foreach (var yVal in edges.Select(e => e.y).Distinct())
            {
                var column = edges.Where(n => n.y == yVal).OrderBy(n => n.x).ToList();

                for (int i = 1; i < column.Count; i++)
                {
                    if (column[i].x != column[i-1].x + 1)
                        sides++;
                }

                sides++;
            }

            edges = nodes.Where(l => !grid.CheckNeighbor(l, GridDirection.South, n => n == regionDef)).ToList();

            foreach (var yVal in edges.Select(e => e.y).Distinct())
            {
                var column = edges.Where(n => n.y == yVal).OrderBy(n => n.x).ToList();

                for (int i = 1; i < column.Count; i++)
                {
                    if (column[i].x != column[i - 1].x + 1)
                        sides++;
                }

                sides++;
            }

            edges = nodes.Where(l => !grid.CheckNeighbor(l, GridDirection.East, n => n == regionDef)).ToList();

            foreach (var xVal in edges.Select(e => e.x).Distinct())
            {
                var column = edges.Where(n => n.x == xVal).OrderBy(n => n.y).ToList();

                for (int i = 1; i < column.Count; i++)
                {
                    if (column[i].y != column[i - 1].y + 1)
                        sides++;
                }

                sides++;
            }

            edges = nodes.Where(l => !grid.CheckNeighbor(l, GridDirection.West, n => n == regionDef)).ToList();

            foreach (var xVal in edges.Select(e => e.x).Distinct())
            {
                var column = edges.Where(n => n.x == xVal).OrderBy(n => n.y).ToList();

                for (int i = 1; i < column.Count; i++)
                {
                    if (column[i].y != column[i - 1].y + 1)
                        sides++;
                }

                sides++;
            }

            return area * sides;
        }

        private static void SetRegionNumber(Grid<(char val, int region)> grid, (int x, int y) node, int regionNumber)
        {
            var newNode = grid[node.x, node.y];
            newNode.region = regionNumber;
            grid[node.x, node.y] = newNode;
        }
    }
}
