using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StringCalculator.BL;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class AddMethodTest
    {
        [Test]
        public void Add_ReturnsDefaultValue()
        {
            var calc = new Calculator();

            var actual = calc.Add(" ");

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Add_ReturnsNumber()
        {
            var calc = new Calculator();

            var actual = calc.Add("1");

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void Add_ReturnsSumOfTwoMembers()
        {
            var calc = new Calculator();

            var actual = calc.Add("1,2");

            Assert.AreEqual(3, actual);
        }
    }
}
