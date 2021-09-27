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
    public class Day13 : BaseDay
    {
        private readonly string input;
        public Day13()
        {
            input = File.ReadAllLines(InputFilePath)[0];
        }

        public override string Solve_1()
        {
            IntcodeProcessor processor = new(input);

            int count = 0;
            while (true)
            {
                processor.RunTilOutput();
                processor.RunTilOutput();
                var done = processor.RunTilOutput();

                if (processor.Output == 2)
                    count++;

                if (done)
                    break;
            }

            return count.ToString();
        }

        public override string Solve_2()
        {
            IntcodeProcessor processor = new(input);
            processor.AlterMemory(0, 2);

            long score = 0;
            long paddle = -1;
            long ball = -1;

            while (true)
            {
                processor.RunTilOutput();
                var x = processor.Output;
                processor.RunTilOutput();
                var y = processor.Output;
                var done = processor.RunTilOutput();

                if ((x, y) == (-1, 0))
                {
                    score = processor.Output;
                }
                else if (processor.Output == 3)
                {
                    paddle = x;

                    processor.inputs.Clear();
                    processor.AddInput(Math.Sign(ball - paddle));
                }
                else if (processor.Output == 4)
                {
                    ball = x;

                    processor.inputs.Clear();
                    processor.AddInput(Math.Sign(ball - paddle));
                }

                if (done)
                {
                    break;
                }
            }

            return score.ToString();
        }
    }
}
