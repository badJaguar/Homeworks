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
           int.TryParse(Console.ReadLine(), out var variable);
           bool used = true;

            switch (variable)
            {
                case 1:
                    {
                        m.PlayMusic();
                        used = true;
                        break;
                    }
                case 2:
                    CallerStorage.ChangeFormatCaller();
                    break;
                default:
                    {
                        Console.WriteLine("Unexpected Case");
                        break;
                    }
            }
            if (used)
            {
                goto Switch;

            }
            Console.ReadKey();
        }
    }
}
