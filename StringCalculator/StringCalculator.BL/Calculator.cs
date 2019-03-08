using System;
using System.Globalization;
using System.Linq;

namespace StringCalculator.BL
{
    public class Calculator
    {
        public int Add(string numbers)
        {
            int t;

            if (numbers is " " || string.IsNullOrEmpty(numbers))
                return 0;

            var split = numbers.Split(',', ' ', ';', '/', '|');
            
            try
            {
                t = (from s in split
                    where s != string.Empty ^ numbers is " " ^ string.IsNullOrEmpty(numbers)
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
