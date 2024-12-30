using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.GameOfLife
{
    internal class Board
    {
        public readonly Cell[,] Cells;

        public int Columns { get { return Cells.GetLength(0); } }
        public int Rows { get {  return Cells.GetLength(1); } }

        public Board(int width, int height)
        {
            Cells = new Cell[width, height];

            ConnectNeighbors();
        }

        public void Advance()
        {
            foreach (var cell in Cells)
            {
                cell.DetermineNextLiveState();
            }

            foreach (var cell in Cells)
            {
                cell.Advance();
            }
        }

        public void LoadPattern(string[] rows, char aliveChar)
        {
            for (int y = 0; y < rows.Length; y++)
            {
                for (int x = 0; x < rows[y].Length; x++)
                {
                    Cells[x, y].IsAlive = rows[y][x] == aliveChar;
                }
            } 
        }

        private void ConnectNeighbors()
        {
            for (int x = 0; x < Columns; x++)
            {
                for (int y = 0; y < Rows; y++)
                {
                    var xL = x - 1;
                    var xR = x + 1;

                    var yT = y - 1;
                    var yB = y + 1;

                    if (xL >= 0)
                        Cells[x, y].Neighbors.Add(Cells[xL, y]);

                    if (xL >= 0 && yT >= 0)
                        Cells[x, y].Neighbors.Add(Cells[xL, yT]);

                    if (xL >= 0 && yB < Rows - 1)
                        Cells[x, y].Neighbors.Add(Cells[xL, yB]);

                    if (xR >= 0)
                        Cells[x, y].Neighbors.Add(Cells[xR, y]);

                    if (xR >= 0 && yT >= 0)
                        Cells[x, y].Neighbors.Add(Cells[xR, yT]);

                    if (xR >= 0 && yB  < Rows - 1)
                        Cells[x, y].Neighbors.Add(Cells[xR, yB]);

                    if (yT >= 0)
                        Cells[x, y].Neighbors.Add(Cells[x, yT]);

                    if (yB < Rows - 1)
                        Cells[x, y].Neighbors.Add(Cells[x, yB]);
                }
            }
        }
    }
}
