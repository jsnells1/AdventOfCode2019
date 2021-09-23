using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Days
{
    public class Day03 : BaseDay
    {
        private string[] input;
        public Day03()
        {
            input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            return MapPath(input[0]).Intersect(MapPath(input[1])).Min(x => Math.Abs(x.X) + Math.Abs(x.Y)).ToString();
        }

        public override string Solve_2()
        {
            var wire1 = MapPath(input[0]);
            var wire2 = MapPath(input[1]);

            var crosses = wire1.Intersect(wire2).ToArray();

            var min = int.MaxValue;
            foreach (var cross in crosses)
            {
                var dist = wire1.IndexOf(cross) + wire2.IndexOf(cross) + 2;
                if (dist < min)
                    min = dist;
            }

            return min.ToString();
        }

        private List<(int X, int Y)> MapPath(string s)
        {
            List<(int X, int Y)> visited = new();

            (int X, int Y) location = (0, 0);
            foreach (var line in s.Split(","))
            {
                var range = Enumerable.Range(1, int.Parse(line[1..]));
                switch (line[0])
                {
                    case 'R':
                    case 'L':
                        foreach (var value in range)
                        {
                            visited.Add((location.X + (line[0] == 'R' ? value : -value), location.Y));
                        }
                        break;
                    case 'U':
                    case 'D':
                        foreach (var value in range)
                        {
                            visited.Add((location.X, location.Y + (line[0] == 'U' ? value : -value)));
                        }
                        break;
                }

                location = visited.Last();
            }
            return visited;
        }
    }
}
