using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.AoC2015
{
    internal partial class Day07 : AoCDay
    {
        private Dictionary<string, ushort> _wires;

        public string Part1()
        {
            _wires = new Dictionary<string, ushort>();

            var input = FileLoader.LoadInputFileAsString(2015, 7, 1);

            var matches = GateParser().Matches(input);

            var a = FindValueRecursive(matches, "a");

            return a.ToString();
        }

        public string Part2()
        {
            _wires = new Dictionary<string, ushort>();

            var input = FileLoader.LoadInputFileAsString(2015, 7, 2);

            var matches = GateParser().Matches(input);

            var a = FindValueRecursive(matches, "a");

            return a.ToString();
        }

        [GeneratedRegex("([a-z\\d]* )* *([A-Z]*) *([a-z\\d]+) -> ([a-z]+)")]
        private static partial Regex GateParser();

        private ushort FindValueRecursive(MatchCollection matches, string node)
        {
            var match = matches.First(m => m.Groups[4].Value == node);

            switch (match.Groups[2].Value.Trim())
            {
                case "NOT":
                    return (ushort)~FindValueRecursive(matches, match.Groups[3].Value);
                case "AND":
                    var val1 = match.Groups[1].Value.Trim();
                    ushort num1;
                    if (!_wires.TryGetValue(val1, out num1) && !ushort.TryParse(val1, out num1))
                        num1 = FindValueRecursive(matches, val1);
                    var val2 = match.Groups[3].Value.Trim();
                    ushort num2;
                    if (!_wires.TryGetValue(val2, out num2) && !ushort.TryParse(val2, out num2))
                        num2 = FindValueRecursive(matches, val2);
                    ushort val = (ushort)(num1 & num2);
                    _wires[node] = val;
                    return val;
                case "OR":
                    val1 = match.Groups[1].Value.Trim();
                    if (!_wires.TryGetValue(val1, out num1) && !ushort.TryParse(val1, out num1))
                        num1 = FindValueRecursive(matches, val1);
                    val2 = match.Groups[3].Value.Trim();
                    if (!_wires.TryGetValue(val2, out num2) && !ushort.TryParse(val2, out num2))
                        num2 = FindValueRecursive(matches, val2);
                    val = (ushort)(num1 | num2);
                    _wires[node] = val;
                    return val;
                case "RSHIFT":
                    val1 = match.Groups[1].Value.Trim();
                    if (!_wires.TryGetValue(val1, out num1) && !ushort.TryParse(val1, out num1))
                        num1 = FindValueRecursive(matches, val1);
                    val2 = match.Groups[3].Value.Trim();
                    if (!_wires.TryGetValue(val2, out num2) && !ushort.TryParse(val2, out num2))
                        num2 = FindValueRecursive(matches, val2);
                    val = (ushort)(num1 >> num2);
                    _wires[node] = val;
                    return val;
                case "LSHIFT":
                    val1 = match.Groups[1].Value.Trim();
                    if (!_wires.TryGetValue(val1, out num1) && !ushort.TryParse(val1, out num1))
                        num1 = FindValueRecursive(matches, val1);
                    val2 = match.Groups[3].Value.Trim();
                    if (!_wires.TryGetValue(val2, out num2) && !ushort.TryParse(val2, out num2))
                        num2 = FindValueRecursive(matches, val2);
                    val = (ushort)(num1 << num2);
                    _wires[node] = val;
                    return val;
                default:
                    val1 = match.Groups[3].Value.Trim();
                    if (!_wires.TryGetValue(val1, out num1) && !ushort.TryParse(val1, out num1))
                        num1 = FindValueRecursive(matches, val1);
                    _wires[node] = num1;
                    return num1;
            }
        }
    }
}
