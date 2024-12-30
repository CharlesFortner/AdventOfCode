using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    internal class FileLoader
    {
        public static string LoadInputFileAsString(int year, int day, int part)
        {
            return File.ReadAllText(Path.Combine(Constants.InputDirectory, year.ToString(), $"Day {day} Part {part}.txt"));
        }

        public static IEnumerable<string> LoadInputFileAsList(int year, int day, int part)
        {
            return File.ReadLines(Path.Combine(Constants.InputDirectory, year.ToString(), $"Day {day} Part {part}.txt"));
        }
    }
}
