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
            var url = "https://metanit.com";
            var p = Task.Run(async () => await parser.ParseHtml(url));
            var links = parser.GetRefs(url);
            foreach (var link in links.Result)
            {
                Task.Run(async () => await parser.ParseHtml(link));
            }

            Console.ReadKey();
        }

       
    }
}
