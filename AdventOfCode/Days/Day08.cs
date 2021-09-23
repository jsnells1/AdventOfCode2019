using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day08 : BaseDay
    {
        private readonly string input;
        private readonly int WIDE = 25;
        private readonly int TALL = 6;
        public Day08()
        {
            input = File.ReadAllLines(InputFilePath)[0];
        }

        public override string Solve_1()
        {
            var temp = input;
            var layers = new List<string>();

            while (temp != string.Empty)
            {
                layers.Add(temp[..(WIDE * TALL)]);
                temp = temp[(WIDE * TALL)..];
            }

            string min = string.Empty;
            int minCount = int.MaxValue;
            foreach (var layer in layers)
            {
                int count = layer.Count(x => x == '0');
                if (count < minCount)
                {
                    min = layer;
                    minCount = count;
                }
            }

            int ones = 0;
            int twos = 0;

            foreach (var c in min)
            {
                if (c == '1') ones++;
                else if (c == '2') twos++;
            }

            return (ones * twos).ToString();
        }

        public override string Solve_2()
        {
            var temp = input;
            var layers = new List<string>();

            while (temp != string.Empty)
            {
                layers.Add(temp[..(WIDE * TALL)]);
                temp = temp[(WIDE * TALL)..];
            }

            string msg = string.Empty;
            for (int i = 0; i < (WIDE * TALL); i++)
            {
                if (i > 0 && i % WIDE == 0)
                {
                    msg += "\n";
                }

                var bit = layers.Select(x => x[i]).First(x => x < '2');
                msg += (bit == '1') ? '#' : '.';
            }

            return msg;
        }
    }
}
