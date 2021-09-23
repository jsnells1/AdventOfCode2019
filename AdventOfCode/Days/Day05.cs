using AdventOfCode.Helpers;
using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day05 : BaseDay
    {
        private string[] input;
        public Day05()
        {
            input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var processor = new IntcodeProcessor(input[0].Split(",").Select(x => int.Parse(x)).ToArray(), 1);
            processor.RunProgram();
            return processor.Output.ToString();
        }

        public override string Solve_2()
        {
            var processor = new IntcodeProcessor(input[0].Split(",").Select(x => int.Parse(x)).ToArray(), 5);
            processor.RunProgram();
            return processor.Output.ToString();
        }
    }
}
