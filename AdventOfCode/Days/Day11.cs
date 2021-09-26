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
    public class Day11 : BaseDay
    {
        private readonly string input;
        public Day11()
        {
            input = File.ReadAllLines(InputFilePath)[0];
        }

        public override string Solve_1()
        {
            IntcodeProcessor processor = new(input);
            HashSet<(int, int)> seen = new();
            HashSet<(int, int)> whites = new();

            bool done = false;

            var position = (0, 0);
            var movement = (1, 0);

            while (!done)
            {
                processor.AddInput(whites.Contains(position) ? 1 : 0);

                processor.RunTilOutput();

                if (processor.Output == 1)
                {
                    whites.Add(position);
                }
                else
                {
                    whites.Remove(position);
                }

                seen.Add(position);
                done = processor.RunTilOutput();
                movement = Rotate(movement, processor.Output);

                position.Item1 += movement.Item1;
                position.Item2 += movement.Item2;
            }

            return seen.Count.ToString();
        }

        public override string Solve_2()
        {
            IntcodeProcessor processor = new(input);
            HashSet<(int, int)> whites = new(new[] { (0, 0) });

            bool done = false;

            var position = (0, 0);
            var movement = (0, 1);

            while (!done)
            {
                processor.AddInput(whites.Contains(position) ? 1 : 0);

                processor.RunTilOutput();

                if (processor.Output == 1)
                {
                    whites.Add(position);
                }
                else
                {
                    whites.Remove(position);
                }

                done = processor.RunTilOutput();
                movement = Rotate(movement, processor.Output);

                position.Item1 += movement.Item1;
                position.Item2 += movement.Item2;
            }

            string output = "";
            int minI = whites.Min(x => x.Item1);
            int maxI = whites.Max(x => x.Item1) - 1;
            int minJ = whites.Min(x => x.Item2);
            int maxJ = whites.Max(x => x.Item2);

            for (int j = maxJ; j >= minJ; j--)
            {
                for (int i = minI; i < maxI; i++)
                {
                    if (whites.Contains((i, j)))
                    {
                        output += '#';
                    }
                    else
                    {
                        output += '.';
                    }
                }
                output += "\n";
            }

            return output;
        }

        private (int, int) Rotate((int, int) movement, long dir)
        {
            // Facing up/down
            if (movement.Item1 == 0)
            {
                if (movement.Item2 == 1)
                {
                    if (dir == 0)
                    {
                        return (-1, 0);
                    }
                    else
                    {
                        return (1, 0);
                    }
                }
                else
                {
                    if (dir == 0)
                    {
                        return (1, 0);
                    }
                    else
                    {
                        return (-1, 0);
                    }
                }
            }
            // Facing left/right
            else
            {
                if (movement.Item1 == 1)
                {
                    if (dir == 0)
                    {
                        return (0, 1);
                    }
                    else
                    {
                        return (0, -1);
                    }
                }
                else
                {
                    if (dir == 0)
                    {
                        return (0, -1);
                    }
                    else
                    {
                        return (0, 1);
                    }
                }
            }
        }
    }
}
