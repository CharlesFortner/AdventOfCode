using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    internal class Grid<T>
    {
        public T[,] Data { get; set; }
        public int X { get { return Data.GetLength(0); } }
        public int Y { get { return Data.GetLength(1); } }


        public Grid(int x, int y, T defaultValue)
        {
            Data = new T[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Data[i, j] = defaultValue;
                }
            }
        }
        public Grid(int x, int y)
        {
            Data = new T[x, y];
        }

        public Grid(T[,] grid)
        {
            Data = grid;
        }

        public T this[int x, int y]
        {
            get { return Data[x, y]; }
            set { Data[x, y] = value; }
        }

        public IEnumerable<TOut> Cast<TOut>()
        {
            return Data.Cast<TOut>();
        }

        public IEnumerable<T> GetNeighbors(int x, int y)
        {
            var neighbors = new List<T>();

            if (x < 0 || x >= X)
                throw new ArgumentOutOfRangeException(nameof(x), "The given coordinate is not in the grid");

            if (y < 0 || y >= Y)
                throw new ArgumentOutOfRangeException(nameof(y), "The given coordinate is not in the grid");

            if (x > 0)
            {
                if (y > 0)
                    neighbors.Add(Data[x - 1, y - 1]);

                neighbors.Add(Data[x - 1, y]);

                if (y < Data.GetLength(1) - 1)
                    neighbors.Add(Data[x - 1, y + 1]);
            }

            if (y < Data.GetLength(1) - 1)
                neighbors.Add(Data[x, y + 1]);

            if (x < Data.GetLength(0) - 1)
            {
                if (y < Data.GetLength(1) - 1)
                    neighbors.Add(Data[x + 1, y + 1]);

                neighbors.Add(Data[x + 1, y]);

                if (y > 0)
                    neighbors.Add(Data[x + 1, y - 1]);
            }

            if (y > 0)
                neighbors.Add(Data[x, y - 1]);

            return neighbors;
        }

        public Grid<T> Clone()
        {
            var x = Data.GetLength(0);
            var y = Data.GetLength(1);

            var clone = new Grid<T>(x, y);

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    clone[i, j] = Data[i, j];
                }
            }

            return clone;
        }

        public void RotateGrid(bool clockwise)
        {
            var newGrid = new T[Y, X]; ;

            for (int i = 0; i < X; i++)
            {
                for (int j = 0; j < Y; j++)
                {
                    if (clockwise)
                        newGrid[j, X - i - 1] = Data[i, j];
                    else
                        newGrid[Y - j - 1, i] = Data[i, j];
                }
            }

            Data = newGrid;
        }

        public (int x, int y) IndexOf(T val)
        {
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (Data[i,j].Equals(val))
                        return (i,j);
                }
            }
            return (-1, -1);
        }

        public IEnumerable<(int x, int y)> IndicesOf(T val)
        {
            var indices = new List<(int x, int y)>();

            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (Data[i, j].Equals(val))
                        indices.Add((i,j));
                }
            }

            return indices;
        }

        public int Count(T val)
        {
            var sum = 0;

            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (Data[i,j].Equals(val))
                        sum++;
                }
            }

            return sum;
        }

        public int Count(Func<T, bool> predicate)
        {
            var sum = 0;

            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (predicate(Data[i,j]))
                        sum++;
                }
            }

            return sum;
        }

        public bool Any(Func<T, bool> predicate)
        {
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (predicate(Data[i, j]))
                        return true;
                }
            }

            return false;
        }

        public bool All(Func<T, bool> predicate)
        {
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (!predicate(Data[i, j]))
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns the index of the first grid location that matches the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>Index of the found location. (-1, -1) if no location matches.</returns>
        public (int x, int y) IndexOf(Func<T, bool> predicate)
        {
            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (predicate(Data[i, j]))
                        return (i, j);
                }
            }

            return (-1, -1);
        }
        public IEnumerable<(int x, int y)> IndicesOf(Func<T, bool> predicate)
        {
            var indices = new List<(int x, int y)>();

            for (int i = 0; i < X; ++i)
            {
                for (int j = 0; j < Y; ++j)
                {
                    if (predicate(Data[i, j]))
                        indices.Add((i, j));
                }
            }

            return indices;
        }

        public T? GetNeighbor((int x, int y) location, GridDirection dir)
        {
            if (!Enum.IsDefined(dir))
                throw new ArgumentOutOfRangeException(nameof(dir));

            var success = TryGetNeighbor(location, dir, out var neighbor);

            if (success)
                return neighbor;

            throw new IndexOutOfRangeException("Target location is outside the grid");
        }

        public bool TryGetNeighbor((int x, int y) location, GridDirection dir, out T? neighbor)
        {
            switch (dir)
            {
                case GridDirection.North:
                    if (location.y > 0)
                    {
                        neighbor = Data[location.x, location.y - 1];
                        return true;
                    }
                    break;
                case GridDirection.NorthEast:
                    if (location.y > 0 && location.x < X - 1)
                    {
                        neighbor = Data[location.x + 1, location.y - 1];
                        return true;
                    }
                    break;
                case GridDirection.East:
                    if (location.x < X - 1)
                    {
                        neighbor = Data[location.x + 1, location.y];
                        return true;
                    }
                    break;
                case GridDirection.SouthEast:
                    if (location.y < Y - 1 && location.x < X - 1)
                    {
                        neighbor = Data[location.x + 1, location.y + 1];
                        return true;
                    }
                    break;
                case GridDirection.South:
                    if (location.y < Y - 1)
                    {
                        neighbor = Data[location.x, location.y + 1];
                        return true;
                    }
                    break;
                case GridDirection.SouthWest:
                    if (location.y < Y - 1 && location.x > 0)
                    {
                        neighbor = Data[location.x - 1, location.y + 1];
                        return true;
                    }
                    break;
                case GridDirection.West:
                    if (location.x > 0)
                    {
                        neighbor = Data[location.x - 1, location.y];
                        return true;
                    }
                    break;
                case GridDirection.NorthWest:
                    if (location.y > 0 && location.x > 0)
                    {
                        neighbor = Data[location.x - 1, location.y - 1];
                        return true;
                    }
                    break;
            }

            neighbor = default;

            return false;
        }

        public bool TryGetNeighbor((int x, int y) location, GridDirection direction, Func<T?, bool> predicate, out T? neighbor)
        {
            if (!TryGetNeighbor(location, direction, out neighbor))
                return false;

            if (predicate(neighbor))
                return true;

            return false;
        }

        public bool TryGetNeighborLocation((int x, int y) location, GridDirection direction, out (int x, int y) neighbor)
        {
            switch (direction)
            {
                case GridDirection.North:
                    if (location.y > 0)
                    {
                        neighbor = (location.x, location.y - 1);
                        return true;
                    }
                    break;
                case GridDirection.NorthEast:
                    if (location.y > 0 && location.x < X - 1)
                    {
                        neighbor = (location.x + 1, location.y - 1);
                        return true;
                    }
                    break;
                case GridDirection.East:
                    if (location.x < X - 1)
                    {
                        neighbor = (location.x + 1, location.y);
                        return true;
                    }
                    break;
                case GridDirection.SouthEast:
                    if (location.y < Y - 1 && location.x < X - 1)
                    {
                        neighbor = (location.x + 1, location.y + 1);
                        return true;
                    }
                    break;
                case GridDirection.South:
                    if (location.y < Y - 1)
                    {
                        neighbor = (location.x, location.y + 1);
                        return true;
                    }
                    break;
                case GridDirection.SouthWest:
                    if (location.y < Y - 1 && location.x > 0)
                    {
                        neighbor = (location.x - 1, location.y + 1);
                        return true;
                    }
                    break;
                case GridDirection.West:
                    if (location.x > 0)
                    {
                        neighbor = (location.x - 1, location.y);
                        return true;
                    }
                    break;
                case GridDirection.NorthWest:
                    if (location.y > 0 && location.x > 0)
                    {
                        neighbor = (location.x - 1, location.y - 1);
                        return true;
                    }
                    break;
            }

            neighbor = (-1, -1);

            return false;
        }

        public bool TryGetNeighborLocation((int x, int y) location, GridDirection direction, Func<T?, bool> predicate, out (int x, int y) neighbor)
        {
            if (!TryGetNeighborLocation(location, direction, out neighbor))
                return false;

            if (predicate(Data[neighbor.x, neighbor.y]))
                return true;

            return false;
        }

        public bool CheckNeighbor((int x, int y) location, GridDirection direction, Func<T?, bool> predicate)
        {
            if (!TryGetNeighbor(location, direction, out var neighbor))
                return false;

            if (predicate(neighbor))
                return true;

            return false;
        }
    }

    internal enum GridDirection
    {
        North = 0,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }
}
