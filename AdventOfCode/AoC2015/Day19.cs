using AdventOfCode.Utils;
using AdventOfCode.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day19 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 19, 1);

            var medMolecule = input.Last();

            var transformStrings = input.Where(s => s.Contains("=>"));

            Dictionary<string, List<string>> transforms = [];

            foreach (var transform in transformStrings)
            {
                var match = TransformParser().Match(transform);

                var source = match.Groups[1].Value;
                var result = match.Groups[2].Value;

                if (!transforms.ContainsKey(source))
                    transforms.Add(source, []);

                transforms[source].Add(result);
            }

            var molecules = FabricateMolecules(medMolecule, transforms).ToArray();

            var possibilities = molecules.Length;

            return possibilities.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2015, 19, 1);

            var medMolecule = input.Last();

            var transformStrings = input.Where(s => s.Contains("=>"));

            Dictionary<string, string> transforms = [];

            foreach (var transform in transformStrings)
            {
                var match = TransformParser().Match(transform);

                var source = match.Groups[1].Value;
                var result = match.Groups[2].Value;

                transforms.Add(result, source);
            }

            int steps = 0;
            List<string> molecules = [medMolecule];

            while (!molecules.Contains("e"))
            {
                steps++;
                Console.WriteLine($"Step: {steps,3}, Count: {molecules.Count,12}");
                molecules = DefabricateMolecules(molecules, transforms).ToList();
            }

            return steps.ToString();
        }


        private IEnumerable<string> DefabricateMolecules(IEnumerable<string> molecules, Dictionary<string, string> transforms)
        {
            HashSet<string> resultMolecules = [];

            foreach ( var molecule in molecules)
            {
                if (!molecule.StartsWith("Th") &&
                    !molecule.StartsWith('B') &&
                    !molecule.StartsWith("Ti") &&
                    !molecule.StartsWith('P') &&
                    !molecule.StartsWith("Si") &&
                    !molecule.StartsWith('C') &&
                    !molecule.StartsWith('O') &&
                    !molecule.StartsWith('H') &&
                    !molecule.StartsWith('N'))
                    continue;

                for (int i = 0; i < molecule.Length; i++)
                {
                    var endMolecule = molecule[i..];
                    var startMolecule = molecule[..i];

                    foreach (var key in transforms.Keys)
                    {
                        if (!endMolecule.StartsWith(key))
                            continue;

                        var newEnd = endMolecule.ReplaceFirst(key, transforms[key]);

                        resultMolecules.Add(startMolecule + newEnd);
                    }
                }
            }

            return resultMolecules;
        }

        private IEnumerable<string> FabricateMolecules(string molecule, Dictionary<string, List<string>> transforms)
        {
            HashSet<string> resultMolecules = [];

            for (int i = 0; i < molecule.Length; i++)
            {
                var element = "";
                var endMolecule = molecule[i..];
                var startMolecule = molecule[..i];

                foreach (var key in transforms.Keys)
                {
                    if (endMolecule.StartsWith(key))
                    {
                        element = key;
                        break;
                    }
                }

                if (element == "")
                    continue;

                foreach (var transform in transforms[element])
                {
                    var newEnd = endMolecule.ReplaceFirst(element, transform);

                    resultMolecules.Add(startMolecule + newEnd);
                }
            }

            return resultMolecules;
        }


        [GeneratedRegex("(\\w+) => (\\w+)")]
        private static partial Regex TransformParser();
    }
}
