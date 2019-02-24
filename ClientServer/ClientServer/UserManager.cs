using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class UserManager
    {
        public string Name { get; set; }

        public void User(Socket client, string name)
        {
            Generate();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"\n'{this.Name}' connected to server\n");
            Console.ForegroundColor = ConsoleColor.Gray;

            var names = new Dictionary<int, string> {{client.GetHashCode(), Name}};
            Console.WriteLine(string.Join(" ", names.Values));

            while (true)
            {
                try
                {
                    var message = new byte[1024];
                    var size = client.Receive(message);
                    client.Send(message, 0, size, SocketFlags.None);
                    Console.WriteLine($"{this.Name}: {Encoding.UTF8.GetString(message, 0, size)}");
                }

                catch (SocketException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{this.Name} leaved a chat\n");
                    Console.ResetColor();
                    return;
                }

            }
        }

        private void Generate() => this.Name = new Bogus.Person().UserName;
    }
}