using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class UserManager
    {
        public void User(Socket client, string name, Dictionary<int, string> myDict) 
        {
            var generator = new NameGenerator();
            generator.Generate();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n'{generator.Name}' connected to server\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            
            while (true)
            {
                try
                {
                    var message = new byte[1024];
                    var size = client.Receive(message);
                    client.Send(message, 0, size, SocketFlags.None);
                    Console.WriteLine($"{generator.Name}: {Encoding.UTF8.GetString(message, 0, size)}");

                    //Console.WriteLine(generator.WriteNames());
                    if (!myDict.ContainsKey(generator.Name.GetHashCode()))
                    {
                        myDict.Add(generator.Name.GetHashCode(), generator.Name);
                    }
                    Console.WriteLine(generator.WriteNames());
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
    }
}