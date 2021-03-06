using AdventOfCode.Days;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdventOfCode.Tests.Days
{
    [TestClass]
    public class Day01_Tests
    {
        private readonly Day01 day = new();

        [TestMethod]
        public void Test_Part1()
        {
            Assert.AreEqual(day.Solve_1(), "3376997");
        }

        [TestMethod]
        public void Test_Part2()
        {
            Assert.AreEqual(day.Solve_2(), "5062623");
        }
    }
}
