using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.GameOfLife
{
    internal class Cell
    {
        public bool IsAlive;
        public readonly List<Cell> Neighbors = [];

        private bool IsAliveNext;
        public void DetermineNextLiveState()
        {
            int liveNeighbors = Neighbors.Count(x => x.IsAlive);

            if (IsAlive)
                IsAliveNext = liveNeighbors == 2 || liveNeighbors == 3;
            else
                IsAliveNext = liveNeighbors == 3;
        }

        public void Advance()
        {
            IsAlive = IsAliveNext;
        }
    }
}
