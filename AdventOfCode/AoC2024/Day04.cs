using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day04 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 4, 1).ToArray();

            //input = new string[] { "MMMSXXMASM",
            //                       "MSAMXMSMSA",
            //                       "AMXSXMAAMM",
            //                       "MSAMASMSMX",
            //                       "XMASAMXAMM",
            //                       "XXAMMXXAMA",
            //                       "SMSMSASXSS",
            //                       "SAXAMASAAA",
            //                       "MAMMMXMMMM",
            //                       "MXMXAXMASX" };

            var count = CountHorizontal(input);

            count += CountVertical(input);

            count += CountDiagonal(input);

            return count.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 4, 1).ToArray();

            //input = new string[] { "MMMSXXMASM",
            //                       "MSAMXMSMSA",
            //                       "AMXSXMAAMM",
            //                       "MSAMASMSMX",
            //                       "XMASAMXAMM",
            //                       "XXAMMXXAMA",
            //                       "SMSMSASXSS",
            //                       "SAXAMASAAA",
            //                       "MAMMMXMMMM",
            //                       "MXMXAXMASX" };


            return CountMas(input).ToString();
        }

        private int CountHorizontal(IEnumerable<string> input)
        {
            var count = 0;

            foreach (var line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    try
                    {
                        if (line[i] == 'X' &&
                            line[i + 1] == 'M' &&
                            line[i + 2] == 'A' &&
                            line[i + 3] == 'S')
                            count++;
                    }
                    catch { }
                    try
                    {
                        if (line[i] == 'X' &&
                            line[i - 1] == 'M' &&
                            line[i - 2] == 'A' &&
                            line[i - 3] == 'S')
                            count++;
                    }
                    catch { }
                }
            }

            return count;
        }

        private int CountVertical(string[] input)
        {
            var count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    try
                    {
                        if (input[i][j] == 'X' &&
                            input[i + 1][j] == 'M' &&
                            input[i + 2][j] == 'A' &&
                            input[i + 3][j] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                    try
                    {
                        if (input[i][j] == 'X' &&
                            input[i - 1][j] == 'M' &&
                            input[i - 2][j] == 'A' &&
                            input[i - 3][j] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                }
            }

            return count;
        }

        private int CountDiagonal(string[] input)
        {
            var count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    try
                    {
                        if (input[i][j] == 'X' &&
                            input[i + 1][j + 1] == 'M' &&
                            input[i + 2][j + 2] == 'A' &&
                            input[i + 3][j + 3] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                    try
                    {
                        if (input[i][j] == 'X' &&
                            input[i - 1][j + 1] == 'M' &&
                            input[i - 2][j + 2] == 'A' &&
                            input[i - 3][j + 3] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                    try
                    {
                        if (input[i][j] == 'X' &&
                            input[i + 1][j - 1] == 'M' &&
                            input[i + 2][j - 2] == 'A' &&
                            input[i + 3][j - 3] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                    try
                    {
                        if (input[i][j] == 'X' &&
                            input[i - 1][j - 1] == 'M' &&
                            input[i - 2][j - 2] == 'A' &&
                            input[i - 3][j - 3] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                }
            }

            return count;
        }

        private int CountMas(string[] input)
        {
            var count = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    try
                    {
                        if (input[i][j] == 'A' &&
                            input[i + 1][j + 1] == 'M' &&
                            input[i - 1][j + 1] == 'M' &&
                            input[i + 1][j - 1] == 'S' &&
                            input[i - 1][j - 1] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                    try
                    {
                        if (input[i][j] == 'A' &&
                            input[i + 1][j - 1] == 'M' &&
                            input[i - 1][j - 1] == 'M' &&
                            input[i + 1][j + 1] == 'S' &&
                            input[i - 1][j + 1] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                    try
                    {
                        if (input[i][j] == 'A' &&
                            input[i + 1][j + 1] == 'M' &&
                            input[i + 1][j - 1] == 'M' &&
                            input[i - 1][j + 1] == 'S' &&
                            input[i - 1][j - 1] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                    try
                    {
                        if (input[i][j] == 'A' &&
                            input[i -+ 1][j + 1] == 'M' &&
                            input[i - 1][j - 1] == 'M' &&
                            input[i + 1][j + 1] == 'S' &&
                            input[i + 1][j - 1] == 'S')
                        {
                            count++;
                        }
                    }
                    catch { }
                }
            }

            return count;
        }
    }
}
