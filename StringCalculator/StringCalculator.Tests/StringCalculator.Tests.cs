using System;
using NUnit.Framework;
using StringCalculator.BL;

namespace StringCalculator.Tests
{
    [TestFixture]
    public class AddMethodTest
    {
        [Test]
        public void Add_Whitespace_DefaultValue()
        {
            var calc = new Calculator();
            // Act.
            var actual = calc.Add(" ");
            //Assert.
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Add_SingleNumber_Number()
        {
            var calc = new Calculator();
            var actual = calc.Add("1");

            Assert.AreEqual(1, actual);
        }

        [Test]
        public void Add_TwoNumbersWithCommaDivider_SumOfTwoNumbers()
        {
            var calc = new Calculator();
            var actual = calc.Add("1,2");

            Assert.AreEqual(3, actual);
        }

        [Test]
        public void Add_ThreeNumbersWithCommaDivider_SumOfThreeNumbers()
        {
            var calc = new Calculator();
            var actual = calc.Add("1,2,3");

            Assert.AreEqual(6, actual);
        }

        [Test]
        public void Add_AnyDivider_SumOfNumbers()
        {
            var calc = new Calculator();
            var actual = calc.Add("1/ 2//3;4");

            Assert.AreEqual(10, actual);
        }

        [Test]
        public void Add_StringEmpty_DefaultValue()
        {
            var calc = new Calculator();
            var actual = calc.Add(string.Empty);

            Assert.AreEqual(0, actual);
        }

        [Test]
        public void Add_Null_DefaultValue()
        {
            var calc = new Calculator();
            var actual = calc.Add(null);

            Assert.IsNotNull(actual);
        }

        [Test]
        public void Add_OverflowException_DefaultValue()
        {
            var calc = new Calculator();
            var actual = calc.Add("99999999999999999999999999999999999999999999");

            Assert.AreEqual(0, actual);
        }

        //[Test]
        //public void Add_OverflowException_Handled()
        //{
        //    Assert.That(Add_OverflowException_DefaultValue, Throws.Nothing);
        //}

        [Test]
        public void Add_OverflowException_MessageAndDefaultValue()
        {
            var calc = new Calculator();

            var ex = new OverflowException();
            var message = ex.Message;
            var actual = calc.Add("99999999999999999999999999999999999999999999");

            Assert.AreEqual(0, actual, message, ex );
        }
    }
}
