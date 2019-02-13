using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultithreadingWorkshop
{
    class Program
    {
        static void Main()
        {
            var randomArray = new Random();
            var randomNum = new Random();
            var bytes = new byte[10];
            randomArray.NextBytes(bytes);

            var arr = TasksChain.DisplayArray(bytes).Result;
            var rand = randomNum.Next(1, 9);
            Console.WriteLine($"Multiplier is {rand}\n");

            Console.WriteLine($"{arr}\n" +
                              $"{ArrayMultiplierAsyncCaller()}\n" +
                              $"{OrderedAsyncCaller()}\n" +
                              $"{AvgResultAsyncCaller()}");
            Console.ReadKey();

            string ArrayMultiplierAsyncCaller() => TasksChain
                .ArrayMultiplierAsync(arr, rand).Result;

            string OrderedAsyncCaller() => TasksChain
                .OrderedAsync(TasksChain
                .ArrayMultiplierAsync(arr, rand).Result).Result;

            string AvgResultAsyncCaller() => TasksChain.AvgResultAsync(TasksChain
                .OrderedAsync(TasksChain.ArrayMultiplierAsync(arr, rand)
                .Result).Result).Result;
        }
    }

    static class TasksChain
    {
        public static Task<string> DisplayArray(byte[] numbers) =>

             Task.FromResult(string.Join(" ", (
                from n in numbers
                select n).AsParallel()));

        public static Task<string> ArrayMultiplierAsync(string numbers, int num) =>

             Task.FromResult(string.Join(" ", (
                from n in numbers.Split()
                let parsed = int.Parse(n)
                select parsed * num).ToArray()));

        public static Task<string> OrderedAsync(string numbers) =>

        Task.FromResult(string.Join(" ",
                    from n in numbers.Split()
                    let parsed = int.Parse(n)
                    orderby parsed descending
                    select parsed));

        public static Task<string> AvgResultAsync(string numbers) =>

        Task.FromResult(string.Join(" ",
                (from n in numbers.Split()
                 let parsed = double.Parse(n)
                 select parsed).Average()));
    }
}
