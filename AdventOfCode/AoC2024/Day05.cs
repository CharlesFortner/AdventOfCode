using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2024
{
    internal class Day05 : AoCDay
    {
        public string Part1()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 5, 1);

            //input = "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13\r\n\r\n75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47".Split("\r\n");

            var orderingRules = new Dictionary<int, List<int>>();

            var sum = 0;

            foreach (var line in input)
            {
                if (line.Contains('|'))
                {
                    ParsePagePair(line, ref orderingRules);
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var value = GetMiddlePageValue(line, orderingRules);

                sum += value;
            }

            return sum.ToString();
        }

        public string Part2()
        {
            var input = FileLoader.LoadInputFileAsList(2024, 5, 1);

            //input = "47|53\r\n97|13\r\n97|61\r\n97|47\r\n75|29\r\n61|13\r\n75|53\r\n29|13\r\n97|29\r\n53|29\r\n61|53\r\n97|53\r\n61|29\r\n47|13\r\n75|47\r\n97|75\r\n47|61\r\n75|61\r\n47|29\r\n75|13\r\n53|13\r\n\r\n75,47,61,53,29\r\n97,61,53,29,13\r\n75,29,13\r\n75,97,47,61,53\r\n61,13,29\r\n97,13,75,29,47".Split("\r\n");

            var orderingRules = new Dictionary<int, List<int>>();

            var sum = 0;

            foreach (var line in input)
            {
                if (line.Contains('|'))
                {
                    ParsePagePair(line, ref orderingRules);
                    continue;
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                var value = GetFixedMiddlePageValue(line, orderingRules);

                sum += value;
            }

            return sum.ToString();
        }

        private void ParsePagePair(string input, ref Dictionary<int, List<int>> orderingRules)
        {
            var split = input.Split('|');

            var num1 = int.Parse(split[0]);
            var num2 = int.Parse(split[1]);

            if (!orderingRules.ContainsKey(num1))
            {
                orderingRules[num1] = new List<int>();
            }

            orderingRules[num1].Add(num2);
        }

        private int GetMiddlePageValue(string input, Dictionary<int, List<int>> orderingRules)
        {
            var split = input.Split(',').Select(s => int.Parse(s)).ToArray();

            var prevValues = new List<int>();

            for (int i = 0; i < split.Count(); i++)
            {
                if (!orderingRules.ContainsKey(split[i]))
                {
                    prevValues.Add(split[i]);
                    continue;
                }

                foreach (var value in orderingRules[split[i]])
                {
                    if (split.Contains(value) && prevValues.Contains(value))
                        return 0;
                }

                prevValues.Add(split[i]);
            }

            var numPages = split.Length;

            var middle = (numPages - 1) / 2;

            return split[middle];
        }

        private int GetFixedMiddlePageValue(string input, Dictionary<int, List<int>> orderingRules)
        {
            if (GetMiddlePageValue(input, orderingRules) != 0)
                return 0;

            var pages = input.Split(',').Select(s => int.Parse(s)).ToArray();

            var correctedPages = new List<int>();
            var changeMade = false;

            do
            {
                correctedPages = new List<int>();
                changeMade = false;

                for (int i = 0; i < pages.Length; i++)
                {
                    if (!orderingRules.ContainsKey(pages[i]))
                    {
                        correctedPages.Add(pages[i]);
                        continue;
                    }

                    var newIndex = correctedPages.Count;

                    foreach (var value in orderingRules[pages[i]])
                    {
                        if (correctedPages.Contains(value) && correctedPages.IndexOf(value) < newIndex)
                        {
                            newIndex = correctedPages.IndexOf(value);
                            changeMade = true;
                        }
                    }

                    correctedPages.Insert(newIndex, pages[i]);
                }

                pages = correctedPages.ToArray();
            } while (changeMade);

            var numPages = correctedPages.Count;

            var middle = (numPages - 1) / 2;

            return correctedPages[middle];
        }
    }
}
