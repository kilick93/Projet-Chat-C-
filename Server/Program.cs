using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;


namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(localEndPoint);
            newsock.Listen(10);
            Socket client = newsock.Accept();
        }





    }
}
