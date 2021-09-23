using AdventOfCode.Helpers;
using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day02 : BaseDay
    {
        private int[] input;
        public Day02()
        {
            input = File.ReadAllLines(InputFilePath)[0].Split(",").Select(x => int.Parse(x)).ToArray();
        }

        public override string Solve_1()
        {
            var copiedInput = input.ToArray();
            copiedInput[1] = 12;
            copiedInput[2] = 2;

            var intcodeProcessor = new IntcodeProcessor(copiedInput);
            intcodeProcessor.RunProgram();

            return intcodeProcessor.ValueAtZero.ToString();
        }

        public override string Solve_2()
        {
            var processor = new IntcodeProcessor(input.ToArray());
            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    processor.ResetProgram();
                    processor.RunProgram(noun, verb);

                    if (processor.ValueAtZero == 19690720)
                    {
                        return (100 * noun + verb).ToString();
                    }
                }
            }

            return "Not Found";
        }
    }
}
