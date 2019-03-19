using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;

namespace Parser.BL
{
    public class Parser
    {
        public async Task<List<string>> GetRefs(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            var elements = document.QuerySelectorAll("a")
                .OfType<IHtmlAnchorElement>();

            var values = (
                from element in elements
                let attribute = element.GetAttribute("href")
                where attribute != "/" && !(attribute is null)
                select element.Href).Distinct().ToList();
            
            return values;
        }

        public async Task ParseHtml(string url)
        {
            ////url = "https://metanit.com";
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(url);
            var elements = document.QuerySelectorAll("a");
            foreach(var element in elements)
            using (FileStream fs =
                File.Create($"C:\\Users\\aleks\\Desktop\\New folder\\{element.ClassName}.html", 1024))
            {
                var info = new UTF8Encoding(true)
                    .GetBytes(document.ToHtml());

                fs.Write(info, 0, info.Length);
            }
        }
    }
}