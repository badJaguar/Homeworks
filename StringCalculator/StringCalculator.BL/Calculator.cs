using System;
using System.Globalization;
using System.Linq;

namespace StringCalculator.BL
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            int number;

            if (numbers is " " || string.IsNullOrEmpty(numbers))
                return 0;

            try
            {
                number = (from s in numbers.Split(',', ' ', ';', '/', '|', ':', '(') // And many other symbols.
                    where s != string.Empty
                    let parsed = int.Parse(s, NumberStyles.Integer)
                    select parsed).Sum();
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            return number;
        }
    }
}
