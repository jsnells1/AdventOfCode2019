using AoCHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day10 : BaseDay
    {
        private readonly bool[][] input;
        public Day10()
        {
            input = File.ReadAllLines(InputFilePath).Select(x => x.Select(y => y == '#').ToArray()).ToArray();
        }

        public override string Solve_1()
        {
            return GetStationPosition().c.ToString();
        }

        public override string Solve_2()
        {
            var station = GetStationPosition().Item1;
            var p = OrderedPoints(ReferencePoints(station.x, station.y, false)).ElementAt(199);

            return ((p.RealX * 100) + p.RealY).ToString();
        }

        private ((int x, int y), int c) GetStationPosition()
        {
            int max = 0;
            (int, int) station = (0, 0);
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (!input[i][j]) continue;

                    var points = ReferencePoints(j, i, true).Count;

                    if (points > max)
                    {
                        station = (j, i);
                        max = points;
                    }
                }
            }

            return (station, max);
        }

        private IEnumerable<Point> OrderedPoints(List<Point> points)
        {
            var all = points.OrderBy(p => p.Angle).ThenBy(p => p.Dist).ToList();
            var lastDestroyed = all[0];
            yield return lastDestroyed;

            int index = 0;

            while (all.Any())
            {
                all.Remove(lastDestroyed);

                lastDestroyed = all.Skip(index).FirstOrDefault(p => p.Angle > lastDestroyed.Angle);

                if (lastDestroyed == null)
                {
                    lastDestroyed = all[0];
                    index = 0;
                }
                else
                {
                    index = all.IndexOf(lastDestroyed);
                }

                yield return lastDestroyed;
            }
        }

        private List<Point> ReferencePoints(int x, int y, bool part1)
        {
            List<Point> points = new();

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (!input[i][j]) continue;
                    if (i == y && j == x) continue;

                    var np = new Point(j - x, i - y)
                    {
                        RealY = i,
                        RealX = j
                    };

                    if (part1)
                    {
                        if (!points.Any(p => p.Slope == np.Slope && p.Angle == np.Angle))
                        {
                            points.Add(np);
                        }
                    }
                    else
                    {
                        points.Add(np);
                    }
                }
            }

            return points;
        }

        class Point
        {
            public Point(int x, int y)
            {
                X = x;
                Y = y;

                if (X == 0)
                {
                    Slope = (Y < 0) ? double.MaxValue : double.MinValue;
                }
                else
                {
                    Slope = -((double)Y / X);
                }

                Dist = Math.Abs(X) + Math.Abs(Y);

                Angle = Math.Atan2(X, Y) * 180.0 / Math.PI;

                if (Angle < 0)
                {
                    Angle = 360 - Angle;
                }
            }

            public int X { get; set; }
            public int RealY { get; set; }
            public int Y { get; set; }
            public int RealX { get; set; }
            public double Slope { get; set; }
            public int Dist { get; set; }
            public double Angle { get; set; }
        }
    }
}
