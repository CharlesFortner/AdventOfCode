using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    internal class AoCMath
    {
        public static int GreatestCommonDenominator(IEnumerable<int> nums)
        {
            for (int i = nums.Min(Math.Abs); i > 0; i--)
            {
                if (nums.All(n => Math.Abs(n) % i == 0))
                    return i;
            }

            return 1;
        }
    }
}
