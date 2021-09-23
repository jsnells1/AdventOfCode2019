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
    public class Day09 : BaseDay
    {
        private readonly string input;
        public Day09()
        {
            input = File.ReadAllLines(InputFilePath)[0];
        }

        //1102 too low
        public override string Solve_1()
        {
            var processor = new IntcodeProcessor(input, 1);
            processor.RunProgram();
            return processor.Output.ToString();
        }

        public override string Solve_2()
        {
            var processor = new IntcodeProcessor(input, 2);
            processor.RunProgram();
            return processor.Output.ToString();
        }
    }
}
