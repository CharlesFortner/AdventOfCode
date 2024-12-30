using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal partial class Day11 : AoCDay
    {
        public string Part1()
        {
            string password = "hxbxwxba";

            do
            {
                password = IncrementPassword(password);
            } while (!ValidatePassword(password));

            return password;
        }

        public string Part2()
        {
            string password = "hxbxwxba";

            do
            {
                password = IncrementPassword(password);
            } while (!ValidatePassword(password));

            do
            {
                password = IncrementPassword(password);
            } while (!ValidatePassword(password));

            return password;
        }

        private bool ValidatePassword(string password)
        {
            if (password.Contains('i') || password.Contains('o') || password.Contains('l'))
                return false;

            if (!ContainsStraight(password))
                return false;

            if (CountDoubles(password) < 2)
                return false;

            return true;
        }

        private static string IncrementPassword(string password)
        {
            var arr = password.ToCharArray();

            if (arr[^1] != 'z')
            {
                arr[^1] = (char)(arr[^1] + 1);
                return new string(arr);
            }

            var newPassword = IncrementPassword(password[0..^1]) + 'a';

            return newPassword;
        }

        private static bool ContainsStraight(string password)
        {
            for (int i = 0; i < password.Length - 2; i++)
            {
                if (password[i] > 'x')
                    continue;

                if (password[i] != password[i + 1] - 1)
                    continue;

                if (password[i + 1] != password[i + 2] - 1)
                    continue;

                return true;
            }

            return false;
        }

        private static int CountDoubles(string password)
        {
            var matches = DoublesCounter().Matches(password);

            return matches.Count;
        }

        [GeneratedRegex("(\\w)\\1")]
        private static partial Regex DoublesCounter();
    }
}
