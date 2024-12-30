using AdventOfCode.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.AoC2015
{
    internal class Day04 : AoCDay
    {
        public string Part1()
        {
            string key = "bgvyzdsv";

            var md5 = System.Security.Cryptography.MD5.Create();
            int num = 1;

            while (true)
            {
                var bytes = Encoding.ASCII.GetBytes($"{key}{num}");

                var hash = md5.ComputeHash(bytes);

                var hex = Convert.ToHexString(hash);

                if (hex.StartsWith("00000"))
                    return num.ToString();

                num++;
            }
        }

        public string Part2()
        {
            string key = "bgvyzdsv";

            var md5 = System.Security.Cryptography.MD5.Create();
            int num = 1;

            while (true)
            {
                var bytes = Encoding.ASCII.GetBytes($"{key}{num}");

                var hash = md5.ComputeHash(bytes);

                var hex = Convert.ToHexString(hash);

                if (hex.StartsWith("000000"))
                    return num.ToString();

                num++;
            }
        }
    }
}
