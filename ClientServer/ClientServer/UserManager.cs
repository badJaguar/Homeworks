using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Server
{
    public class UserManager
    {
        public void User(Socket client, Dictionary<int, string> usersDict)
        {
            var generator = new NameGenerator();
            generator.Generate();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n'{generator.Name}' connected to server\n");
            Console.ResetColor();

            while (true)
            {
                try
                {
                    var message = new byte[1024];
                    var size = client.Receive(message);
                    client.Send(message, 0, size, SocketFlags.None);
                    var encodedMessage = Encoding.UTF8.GetString(message, 0, size);
                    Console.WriteLine($"{generator.Name}: {encodedMessage}");


                    if (!usersDict.ContainsKey(generator.Name.GetHashCode()))
                    {
                        usersDict.Add(generator.Name.GetHashCode(), generator.Name);
                    }
                }
                catch (SocketException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{generator.Name} leaved a chat\n");
                    Console.ResetColor();
                    return;
                }
            }
        }

        public void ShareMessages(Socket client, string name, Dictionary<int, string> usersDict,
            Dictionary<Dictionary<int, string>, string> mes)
        {
            var generator = new NameGenerator();
            while (true)
            {
                var serverMessage = new byte[1024];

                var size = client.Send(serverMessage);
                var encodedMessage = Encoding.UTF8.GetString(serverMessage, 0, size);

                if (!mes.ContainsKey(usersDict))
                {
                    mes.Add(usersDict, encodedMessage);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{generator.Name}: {encodedMessage}");
                    Console.ResetColor();
                }
            }
        }
    }
}