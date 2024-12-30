using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day10 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 10, 1).ToList();

            //input = ["89010123", "78121874", "87430965", "96549874", "45678903", "32019012", "01329801", "10456732"];

            var grid = new Grid<int>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j] - '0';
                }
            }

            var startLocations = grid.IndicesOf(0);

            var trailheadTotal = 0;

            foreach (var loc in startLocations)
            {
                trailheadTotal += GetTrailheadScore(loc, grid);
            }

            return trailheadTotal.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 10, 1).ToList();

            //input = ["89010123", "78121874", "87430965", "96549874", "45678903", "32019012", "01329801", "10456732"];

            var grid = new Grid<int>(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    grid[i, j] = input[i][j] - '0';
                }
            }

            var startLocations = grid.IndicesOf(0);

            var trailheadTotal = 0;

            foreach (var loc in startLocations)
            {
                trailheadTotal += GetTrailheadRating(loc, grid);
            }

            return trailheadTotal.ToString();
        }

        private int GetTrailheadScore((int x, int y) startLoc, Grid<int> grid)
        {
            var endLocs = new HashSet<(int x, int y)>();

            var nextLocs = new List<(int x, int y)>() { startLoc };

            for (int nextVal = 1; nextVal < 10;  nextVal++)
            {
                var curLocs = nextLocs;
                nextLocs = [];

                foreach (var loc in curLocs)
                {
                    nextLocs.AddRange(GetNextLocs(loc, grid, nextVal));
                }
            }

            foreach (var loc in nextLocs)
            {
                endLocs.Add(loc);
            }

            return endLocs.Count;
        }

        private int GetTrailheadRating((int x, int y) startLoc, Grid<int> grid)
        {
            var nextLocs = new List<(int x, int y)>() { startLoc };

            for (int nextVal = 1; nextVal < 10; nextVal++)
            {
                var curLocs = nextLocs;
                nextLocs = [];

                foreach (var loc in curLocs)
                {
                    nextLocs.AddRange(GetNextLocs(loc, grid, nextVal));
                }
            }

            return nextLocs.Count;
        }

        private static List<(int x, int y)> GetNextLocs((int x, int y) loc, Grid<int> grid, int nextVal)
        {
            var nextLocs = new List<(int x, int y)>();

            if (grid.TryGetNeighbor(loc, GridDirection.North, out var nextLocVal) && nextLocVal == nextVal)
            {
                _ = grid.TryGetNeighborLocation(loc, GridDirection.North, out var nextLoc);
                nextLocs.Add(nextLoc);
            }
            if (grid.TryGetNeighbor(loc, GridDirection.East, out nextLocVal) && nextLocVal == nextVal)
            {
                _ = grid.TryGetNeighborLocation(loc, GridDirection.East, out var nextLoc);
                nextLocs.Add(nextLoc);
            }
            if (grid.TryGetNeighbor(loc, GridDirection.South, out nextLocVal) && nextLocVal == nextVal)
            {
                _ = grid.TryGetNeighborLocation(loc, GridDirection.South, out var nextLoc);
                nextLocs.Add(nextLoc);
            }
            if (grid.TryGetNeighbor(loc, GridDirection.West, out nextLocVal) && nextLocVal == nextVal)
            {
                _ = grid.TryGetNeighborLocation(loc, GridDirection.West, out var nextLoc);
                nextLocs.Add(nextLoc);
            }

            return nextLocs;
        }
    }
}
