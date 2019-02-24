using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public class Starter
    {

        public void StartServer()
        {
            var generator = new NameGenerator();
            var port = 11000;
            const string ipAddress = "127.0.0.1";
            var serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            serverListener.Bind(endPoint);
            serverListener.Listen(100);
            Console.WriteLine("Server started...");

            while (true)
            {
                var manager = new UserManager();
                var socket = serverListener.Accept();

                var userThread = new Thread(() => manager.User(socket, generator.Name));
                userThread.Start();

                if (!userThread.IsAlive)
                {
                    Console.WriteLine($"{userThread} leaved a chat.");
                    userThread.Abort();
                    return;
                }
            }
        }
    }
}