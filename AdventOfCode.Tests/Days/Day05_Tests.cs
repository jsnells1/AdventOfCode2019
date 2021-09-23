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
    public class Day05_Tests
    {
        private readonly Day05 day = new();

        [TestMethod]
        public void Test_Part1()
        {
            Assert.AreEqual(day.Solve_1(), "16209841");
        }

        [TestMethod]
        public void Test_Part2()
        {
            Assert.AreEqual(day.Solve_2(), "8834787");
        }
    }
}
