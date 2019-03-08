using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator.BL
{
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
