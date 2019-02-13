using Bogus;
using System;
using System.Collections.ObjectModel;

namespace SecondWorkshop
{
    class Program
    {
        static void Main()
        {
            var bogus = new Person();
            var words = bogus.Random.WordsArray(10);

            Console.WriteLine(CollectionFiller.Add(words));
            Console.ReadKey();
        }
    }

    public static class CollectionFiller
    {
        public static Collection<string> Add(string[] word) => new Collection<string>
            {
                string.Join(" ", word)
            };
    }
}
