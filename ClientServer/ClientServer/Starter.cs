using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server
{
    public class Starter
    {
        /// <summary>
        /// Activates server.
        /// </summary>
        public void StartServer()
        {
            var namesDict = new Dictionary<int, string>();
            var mesDict = new Dictionary<Dictionary<int, string>, string>();

            const int port = 11000;
            const string ipAddress = "127.0.0.1";

            Listen(ipAddress, port, namesDict);
        }

        /// <summary>
        /// Represents a server listener.
        /// </summary>
        /// <param name="ipAddress">Local IP Address</param>
        /// <param name="port">Local port.</param>
        /// <param name="namesDict">A collection of KeyValuePair where values are names and keys are those hash codes.</param>
        private static void Listen(string ipAddress, int port, Dictionary<int, string> namesDict)
        {
            using (var serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
                serverListener.Bind(endPoint);
                serverListener.Listen(100);
                Console.WriteLine("Server started...");

                while (true)
                {
                    var manager = new UserManager();
                    var socket = serverListener.Accept();
                    var userThread = new Thread(() => manager.User(socket, namesDict));
                    //var messageThread = new Thread(() => manager.ShareMessages(socket, generator.Name, namesDict, mesDict));
                    userThread.Start();
                }
            }
        }
    }
}