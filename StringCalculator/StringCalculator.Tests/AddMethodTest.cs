using System;
using NUnit.Framework;
using StringCalculator.BL;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class AddMethodTest
    {
        public Calculator calc;

        [SetUp]
        public void Init()
        {
            calc = new Calculator();
        }
        [Test]
        public void Add_ReturnsDefaultValue()
        {
            var actual = calc.Add(" ");

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Add_ReturnsNumber()
        {
            var actual = calc.Add("1");

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void Add_ReturnsSumOfTwoMembersWithCommaDivider()
        {
            var actual = calc.Add("1,2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Add_ReturnsSumThreeTwoMembersWithCommaDivider()
        {
            var actual = calc.Add("1,2,3");

            Assert.AreEqual(6, actual);
        }

        [Test]
        public void Add_ReturnsSumOfAnyDividerWithAnyCountOfNums()
        {
            var actual = calc.Add("1/ 2//3;4");

            Assert.AreEqual(10, actual);
        }

        [Test]
        public void Add_ReturnsZeroIfStringEmpty()
        {
            var actual = calc.Add(string.Empty);

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Add_ReturnsZeroIfNull()
        {
            var actual = calc.Add(null);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void Add_ReturnsZeroIfOverflow()
        {
            var actual = calc.Add("99999999999999999999999999999999999999999999");

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Add_DoesNotTrowsOverflowException()
        {
            Assert.That(Add_ReturnsZeroIfOverflow, Throws.Nothing);
        }

        [Test]
        public void Add_ReturnsMessageAndCatchesExceptionIfOverflowException()
        {
            var ex = new OverflowException();
            var message = ex.Message;
            var actual = calc.Add("99999999999999999999999999999999999999999999");

            Assert.AreEqual(0, actual, message, ex );
        }
    }
}
