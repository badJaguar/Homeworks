using System;
using System.Collections.Generic;
using System.Globalization;
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
            calc.Add("");
            calc.Add(" ");
            calc.Add("1,2");
            calc.Add("1\n 2//3;4");
            calc.Add("999999999999999999999999999999999999999999999999999");

            Console.ReadKey();
        }
    }

    public class Calculator
    {
        public int Add(string numbers)
        {
            int t;

            if (numbers is " " || string.IsNullOrEmpty(numbers))
                return 0;
            
            try
            {
                t = (from s in numbers.Split(',', ' ', ';', '/', '|', ':', '(') // And many other symbols.
                where s != string.Empty
                    let parsed = int.Parse(s, NumberStyles.Integer)
                    select parsed).Sum();
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            return t;
        }

    }
}
