using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultithreadingWorkshop
{
    class Program
    {
        static void Main()
        {
            TasksChain t = new TasksChain();
            var randomArray = new Random();
            var randomNum = new Random();
            var bytes = new byte[10];
            randomArray.NextBytes(bytes);

            var arr = t.DisplayArray(bytes).Result;
            var rand = randomNum.Next(1,9);
            Console.WriteLine($"Multiplier is {rand}");
            
            Console.WriteLine();

            var t1 = Task.Run(()=> Console.WriteLine(arr));
            var t2 = Task.Run(()=> Console.WriteLine(t.ArrayMultiplier(arr, rand).Result));
            var t3 = Task.Run(()=> Console.WriteLine(t.Ordered(t.ArrayMultiplier(arr, rand).Result).Result));
            var t4 = Task.Run(()=> Console.WriteLine(t.AvgResult(t.Ordered(t.ArrayMultiplier(arr, rand)
                .Result).Result).Result));
            Console.ReadKey();
        }
    }

    public class TasksChain
    {
        
        public Task<string> DisplayArray(byte[] numbers) =>

             Task.FromResult(string.Join(" ", (
                from n in numbers
                select n).AsParallel()));

        public Task<string> ArrayMultiplier(string numbers, int num) =>

             Task.FromResult(string.Join(" ", (
                from n in numbers.Split()
                let parsed = int.Parse(n)
                select parsed * num).ToArray()))
               .ContinueWith(task => task.Result);

        public Task<string> Ordered(string numbers) =>

        Task.FromResult(string.Join(" ", 
                    from n in numbers.Split()
                    let parsed = int.Parse(n)
                    orderby parsed descending
                    select parsed))
                   .ContinueWith(s=>s.Result);

        public async Task<string> AvgResult(string numbers) =>

        await Task.FromResult(string.Join(" ",
                (from n in numbers.Split()
                 let parsed = double.Parse(n)
                 select parsed).Average()))
                .ConfigureAwait(true);
    }
}
