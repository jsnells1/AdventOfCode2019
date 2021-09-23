using AdventOfCode.Helpers;
using AoCHelper;
using System.IO;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day05 : BaseDay
    {
        private string input;
        public Day05()
        {
            input = File.ReadAllLines(InputFilePath)[0];
        }

        public override string Solve_1()
        {
            var processor = new IntcodeProcessor(input, 1);
            processor.RunProgram();
            return processor.Output.ToString();
        }

        public override string Solve_2()
        {
            var processor = new IntcodeProcessor(input, 5);
            processor.RunProgram();
            return processor.Output.ToString();
        }
    }
}
