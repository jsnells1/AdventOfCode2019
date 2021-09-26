using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class Day12 : BaseDay
    {
        private readonly string[] input;
        public Day12()
        {
            input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            List<Moon> moons = new();
            foreach (var line in input)
            {
                var cordsRaw = line.Split(",");
                var x = cordsRaw[0][3..];
                var y = cordsRaw[1][3..];
                var z = cordsRaw[2][3..^1];

                moons.Add(new Moon(int.Parse(x), int.Parse(y), int.Parse(z)));
            }

            for (int i = 0; i < 1000; i++)
            {
                foreach (var moon in moons)
                {
                    foreach (var other in moons.Where(m => m != moon))
                    {
                        moon.ApplyGravity(other);
                    }
                }

                moons.ForEach(m => m.Move());
            }

            return moons.Sum(x => x.TotalEnergy).ToString();
        }

        public override string Solve_2()
        {
            List<Moon> originalMoons = new();
            foreach (var line in input)
            {
                var cordsRaw = line.Split(",");
                var x = cordsRaw[0][3..];
                var y = cordsRaw[1][3..];
                var z = cordsRaw[2][3..^1];

                originalMoons.Add(new Moon(int.Parse(x), int.Parse(y), int.Parse(z)));
            }

            var statesByDir = new long[3];
            for (int i = 0; i < 3; i++)
            {
                HashSet<(int, int, int, int, int, int, int, int)> states = new();

                List<Moon> moons = new();
                foreach(var moon in originalMoons)
                {
                    moons.Add(new Moon(moon.position[0], moon.position[1], moon.position[2]));
                }

                while (states.Add((moons[0].position[i], moons[0].velocity[i],
                    moons[1].position[i], moons[1].velocity[i],
                    moons[2].position[i], moons[2].velocity[i],
                    moons[3].position[i], moons[3].velocity[i])))
                {

                    foreach (var moon in moons)
                    {
                        foreach (var other in moons.Where(m => moon != m))
                        {
                            moon.ApplyGravity(other);
                        }
                    }

                    moons.ForEach(m => m.Move());
                }

                statesByDir[i] = states.Count;

            }

            return LCM(statesByDir[0], LCM(statesByDir[1], statesByDir[2])).ToString();
        }

        private static long GCF(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private static long LCM(long a, long b)
        {
            return a / GCF(a, b) * b;
        }
    }

    class Moon
    {
        public int[] position;
        public int[] velocity = new[] { 0, 0, 0 };

        public int PotentialEnergy => position.Sum(x => Math.Abs(x));
        public int KineticEnergy => velocity.Sum(x => Math.Abs(x));
        public int TotalEnergy => PotentialEnergy * KineticEnergy;

        public Moon(int x, int y, int z)
        {
            position = new[] { x, y, z };
        }

        public void ApplyGravity(Moon moon)
        {
            velocity[0] += Math.Sign(moon.position[0] - position[0]);
            velocity[1] += Math.Sign(moon.position[1] - position[1]);
            velocity[2] += Math.Sign(moon.position[2] - position[2]);
        }

        public void Move()
        {
            position[0] += velocity[0];
            position[1] += velocity[1];
            position[2] += velocity[2];
        }
    }
}
