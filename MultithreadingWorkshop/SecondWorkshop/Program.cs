using Bogus;
using System;
using System.Collections.ObjectModel;
using System.Threading;

namespace SecondWorkshop
{
    class Program
    {
        static void Main()
        {
            var bogus = new Person();
            var words = bogus.Random.WordsArray(10);

            var thread1 = new Thread(()=> CollectionFiller.AddValues(words));
            var thread2 = new Thread(()=> CollectionFiller.PrintValues(words));
            thread1.Start();
            thread2.Start();
            
            Console.ReadKey();
        }
    }

    public static class CollectionFiller
    {
        public static Collection<string> AddValues(string[] word) =>
            new Collection<string>
            {
                string.Concat(word)
            };

        public static void PrintValues(string[] words) =>
            Console.WriteLine(string.Join("\n",words));
    }
}
