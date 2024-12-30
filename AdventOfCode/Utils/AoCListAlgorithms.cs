using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Utils
{
    public static class AoCListAlgorithms
    {
        public static IEnumerable<IEnumerable<T>> AllPermutations<T>(IEnumerable<T> list, bool returnToStart = false)
        {
            int length = list.Count() - 1;

            var allPerm = new List<List<T>>();

            AllPermutations(list.ToArray(), 0, length, ref allPerm, returnToStart);

            return allPerm;
        }

        private static void AllPermutations<T>(T[] list, int k, int m, ref List<List<T>> output, bool returnToStart = false)
        {
            if (k == m)
            {
                if (returnToStart)
                {
                    list = [.. list, list[0]];
                }

                output ??= [];
                output.Add([.. list]);
                return;
            }

            for (int i = k; i <= m; i++)
            {
                Swap(ref list[k], ref list[i]);
                AllPermutations(list, k + 1, m, ref output, returnToStart);
                Swap(ref list[k], ref list[i]);
            }
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            (a, b) = (b, a);
        }

        public static void SetRange<T>(this List<T> list, T val, int index, int count)
        {
            for (int i = index; i < index + count; i++)
            {
                list[i] = val;
            }
        }
    }
}
