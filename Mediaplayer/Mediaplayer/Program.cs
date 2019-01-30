using MediaData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediaplayer
{
    class Program
    {
        static void Main()
        {
            var start = new AppStarter();
            var m = new Manipulations();
            start.PrintMessage();

            Switch:
            Console.WriteLine($"Menu\nChose a paragraph:\nPress '1' to play\n" +
                              $"Press '2' to stop\nPress '3' to next song\n" +
                              $"press '4' to previous song\nPress '5' to change format\n" +
                              $"Press 'Esc' to exit");
            Console.WriteLine();
            int.TryParse(Console.ReadLine(), out var variable);

            switch (variable)
            {
                case 1:
                    m.PlayMusic();
                    break;
                case 2:
                    Manipulations.Stop();
                    break;
                case 3:
                    Manipulations.ToNext();
                    break;
                case 4:
                    Manipulations.ToPrevious();
                    break;
                case 5:
                    CallerStorage.ChangeFormatCaller();
                    break;

                default:
                    Console.WriteLine("Unexpected Case");
                    break;
            }

            if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                Process.GetCurrentProcess().Kill();

            goto Switch;
        }
    }
}
