using AoCHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    // TODO: Refactor
    public class Day04 : BaseDay
    {
        private string[] input;
        public Day04()
        {
            input = File.ReadAllLines(InputFilePath);
        }

        public override string Solve_1()
        {
            var line = input[0].Split("-");
            int min = int.Parse(line[0]);
            int max = int.Parse(line[1]);

            int count = 0;

            for (; min < max; min++)
            {
                var pass = min.ToString().ToCharArray();
                bool good = true;
                bool hasDouble = false;
                for (int i = 1; i < 6; i++)
                {
                    if (int.Parse("" + pass[i]) < int.Parse("" + pass[i - 1]))
                    {
                        good = false;
                        break;
                    }

                    if (int.Parse("" + pass[i]) == int.Parse("" + pass[i - 1]))
                    {
                        hasDouble = true;
                    }
                }

                if (good && hasDouble)
                    count++;


            }

            return count.ToString();
        }

        public override string Solve_2()
        {
            var line = input[0].Split("-");
            int min = int.Parse(line[0]);
            int max = int.Parse(line[1]);

            int count = 0;

            for (; min < max; min++)
            {
                var pass = min.ToString();
                var check = pass.ToList();
                check.Sort();


                var check2 = pass.GroupBy(c => c).Select(c => new { Char = c.Key, Count = c.Count() });

                //if (pass.SequenceEqual(check) && pass.Any(x => pass.Where(y => y == x).Count() == 2))
                //{
                //    count++;
                //}

                if (pass.SequenceEqual(check) && check2.Any(x => x.Count == 2))
                {
                    count++;
                }
            }

            return count.ToString();
        }
    }
}
