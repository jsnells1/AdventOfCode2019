using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Helpers
{
    static class Enumerables
    {
        public static IEnumerable<T[]> Permutations<T>(T[] values)
        {
            // Credit: https://github.com/encse/adventofcode/blob/6eb936b4fd55cda4acb7540abaa915c1a67a4b7a/2019/Day07/Solution.cs#L45

            IEnumerable<T[]> PermutationsRec(int i)
            {
                if (i == values.Length)
                {
                    yield return values.ToArray();
                }

                for (var j = i; j < values.Length; j++)
                {
                    (values[i], values[j]) = (values[j], values[i]);
                    foreach (var perm in PermutationsRec(i + 1))
                    {
                        yield return perm;
                    }
                    (values[i], values[j]) = (values[j], values[i]);
                }
            }

            return PermutationsRec(0);
        }
    }
}
