using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day06 : BaseDay
    {
        private readonly string[] input;
        private readonly Dictionary<string, string> map = new();
        public Day06()
        {
            input = File.ReadAllLines(InputFilePath);
            foreach (var line in input)
            {
                var sp = line.Split(")");
                map[sp[1]] = sp[0];
            }
        }

        public override string Solve_1()
        {
            int countOrbits(string key)
            {
                int orbits = 1;
                while (map[key] != "COM")
                {
                    orbits++;
                    key = map[key];
                }
                return orbits;
            }

            int orbits = 0;
            foreach (var key in map.Keys)
            {
                orbits += countOrbits(key);
            }

            return orbits.ToString();
        }

        public override string Solve_2()
        {
            List<string> mapPath(string key)
            {
                List<string> path = new();
                while (map[key] != "COM")
                {
                    path.Add(map[key]);
                    key = map[key];
                }
                return path;
            }

            var YOU = mapPath("YOU");
            var SAN = mapPath("SAN");

            var intersect = YOU.Intersect(SAN);

            return (YOU.IndexOf(intersect.First()) + SAN.IndexOf(intersect.First())).ToString();
        }
    }
}
