using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day01 : BaseDay
    {
        private readonly IEnumerable<int> input;
        public Day01()
        {
            input = File.ReadAllLines(InputFilePath).Select(x => int.Parse(x));
        }

        public override string Solve_1()
        {
            return input.Sum(x => x / 3 - 2).ToString();
        }

        public override string Solve_2()
        {
            long total = 0;
            foreach (var value in input)
            {
                int val = value;
                while (val > 0)
                {
                    total += val = Math.Max(0, val / 3 - 2);
                }
            }

            return total.ToString();
        }
    }
}
