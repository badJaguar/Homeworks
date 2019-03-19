using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace ParserHw
{
    class Program
    {
        static void Main()
        {
            var parser = new Parser.BL.Parser();
            //Console.Write("Paste a URL: ");

            //var url = Console.ReadLine();
            var refs = Task.Run(async () => await parser.GetRefs("https://metanit.com"));
            foreach (var c in refs.Result)
            {
                Console.WriteLine(c);
            }
            parser.ParseHtml();
            Console.ReadKey();
        }

       
    }
}
