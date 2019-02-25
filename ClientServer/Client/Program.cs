using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Server;

namespace Client
{
    class Program
    {
        static void Main()
        {
            var port = 11000;
            const string ipAddress = "127.0.0.1";
            using (var clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                clientSocket.Connect(endPoint);
                Console.WriteLine($"You connected to {endPoint}");

                while (true)
                {
                    var message = new Bogus.Person().Random.Words(5);
                    clientSocket.Send(Encoding.UTF8.GetBytes(message));
                    var serverMessage = new byte[1024];
                    var size = clientSocket.Receive(serverMessage);
                    Console.WriteLine($"Me: {Encoding.UTF8.GetString(serverMessage, 0, size)}");
                    Thread.Sleep(5000);
                }
            }
        }
    }
}
