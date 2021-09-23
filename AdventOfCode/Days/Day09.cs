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
        private readonly long[] input;
        public Day09()
        {
            input = File.ReadAllLines(InputFilePath)[0].Split(",").Select(x => long.Parse(x)).ToArray();
        }

        //1102 too low
        public override string Solve_1()
        {
            //var processor = new IntcodeProcessor(input);
            //processor.RunProgram();
            //return processor.Output.ToString();
            throw new NotImplementedException();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}
