using AdventOfCode.Helpers;
using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day07 : BaseDay
    {
        private readonly int[] input;
        public Day07()
        {
            input = File.ReadAllLines(InputFilePath)[0].Split(",").Select(x => int.Parse(x)).ToArray();
        }

        public override string Solve_1()
        {
            var processor = new IntcodeProcessor(input.ToArray());
            int max = 0;

            foreach (var perm in Enumerables.Permutations(new int[] { 0, 1, 2, 3, 4 }))
            {
                processor.AddInput(perm[0], 0);
                processor.RunProgram();
                var outA = processor.Output;
                processor.ResetProgram();

                processor.AddInput(perm[1], outA);
                processor.RunProgram();
                var outB = processor.Output;
                processor.ResetProgram();

                processor.AddInput(perm[2], outB);
                processor.RunProgram();
                var outC = processor.Output;
                processor.ResetProgram();

                processor.AddInput(perm[3], outC);
                processor.RunProgram();
                var outD = processor.Output;
                processor.ResetProgram();

                processor.AddInput(perm[4], outD);
                processor.RunProgram();
                var outE = processor.Output;
                processor.ResetProgram();

                if (outE > max)
                {
                    max = outE;
                }
            }

            return max.ToString();
        }

        public override string Solve_2()
        {
            int max = 0;

            foreach (var perm in Enumerables.Permutations(new int[] { 5, 6, 7, 8, 9 }))
            {
                var proccesors = Enumerable.Range(0, 5).Select(x => new IntcodeProcessor(input, perm[x])).ToList();
                var signals = new int[] { 0, 0, 0, 0, 0 };

                bool running = true;
                while (running)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        proccesors[i].AddInput(signals[(i + 4) % 5]);
                        running = !proccesors[i].RunTilOutput();
                        signals[i] = proccesors[i].Output;
                    }
                }

                if (signals[4] > max)
                {
                    max = signals[4];
                }
            }

            return max.ToString();
        }
    }
}
