using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalculator;

namespace StringCalculator
{
    class Program
    {
        static void Main()
        {
            var calc = new Calculator();
            calc.Add("1,2");
            calc.Add(" ");

            Console.ReadKey();
        }
    }

    public class Calculator
    {
        public int Add(string numbers)
        {
            if (numbers is " " || numbers is "")
                return 0;

            var num = numbers.Split(',');

            var t = (from n in num
                let parsed = int.Parse(n)
                select parsed).Sum();
            return t;
        }

    }
}
