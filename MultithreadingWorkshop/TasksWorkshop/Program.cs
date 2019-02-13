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

            var t1 = Task.Run(() => Console.WriteLine(arr));
            var t2 = Task.Run(() => Console.WriteLine(TasksChain
                .ArrayMultiplierAsync(arr, rand).Result));
            var t3 = Task.Run(() => Console.WriteLine(TasksChain
                .OrderedAsync(TasksChain
                .ArrayMultiplierAsync(arr, rand).Result).Result));
            var t4 = Task.Run(() => Console.WriteLine(TasksChain
                .AvgResultAsync(TasksChain.OrderedAsync(TasksChain.ArrayMultiplierAsync(arr, rand)
                .Result).Result).Result));
            Console.ReadKey();
        }
    }

    public static class TasksChain
    {
        public static Task<string> DisplayArray(byte[] numbers) =>

             Task.FromResult(string.Join(" ", (
                from n in numbers
                select n).AsParallel()));

        public static Task<string> ArrayMultiplierAsync(string numbers, int num) =>

             Task.FromResult(string.Join(" ", (
                from n in numbers.Split()
                let parsed = int.Parse(n)
                select parsed * num).ToArray()))
               .ContinueWith(task => task.Result);

        public static Task<string> OrderedAsync(string numbers) =>

        Task.FromResult(string.Join(" ",
                    from n in numbers.Split()
                    let parsed = int.Parse(n)
                    orderby parsed descending
                    select parsed))
                   .ContinueWith(s => s.Result);

        public static async Task<string> AvgResultAsync(string numbers) =>

        await Task.FromResult(string.Join(" ",
                (from n in numbers.Split()
                 let parsed = double.Parse(n)
                 select parsed).Average()))
                .ConfigureAwait(true);
    }
}
