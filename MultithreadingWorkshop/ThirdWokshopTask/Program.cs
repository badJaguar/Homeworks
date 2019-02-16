using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdWokshopTask
{
    class Program
    {
        static void Main()
        {
            var task1 = new Task(()=>{Console.WriteLine($"First task ID is: {Task.CurrentId}."); });
            var task2 = task1.ContinueWith(TaskRunner).Status == TaskStatus.Faulted &&
                task1.IsCanceled;

            task1.Start();
            Console.ReadKey();
        }

        static void TaskRunner(Task t)
        {
            Console.WriteLine($"Task current ID is: {Task.CurrentId}");
            Console.WriteLine($"Prev task ID is: {t.Id}");
        }
    }
}
