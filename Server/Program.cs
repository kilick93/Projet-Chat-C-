using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread listening = new Thread(listen);
            listening.IsBackground = true;
            listening.Start();
            while (true) ;
        }

        static void listen()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(localEndPoint);
            newsock.Listen(10);
            while (true)
            {
                
                Socket client = newsock.Accept();
            }
        }



    }
}
