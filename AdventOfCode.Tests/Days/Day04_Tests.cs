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
    public class Day04_Tests
    {
        private readonly Day04 day = new();

        [TestMethod]
        public void Test_Part1()
        {
            Assert.AreEqual(day.Solve_1(), "1610");
        }

        [TestMethod]
        public void Test_Part2()
        {
            Assert.AreEqual(day.Solve_2(), "1104");
        }
    }
}
