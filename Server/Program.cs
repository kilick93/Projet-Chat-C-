using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Collections;


namespace Server
{
    public class server
    {
        public ArrayList acceptList = new ArrayList();
        public ArrayList readList = new ArrayList();

        public void start()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Thread readclient = new Thread(new ThreadStart(readMessageClient));
            readclient.Start();
            try
            {
                newsock.Bind(localEndPoint);
                newsock.Listen(10);
                while (true)
                {
                    Socket client = newsock.Accept();
                    Console.WriteLine("Un client se connecte au serveur");
                    acceptList.Add(client);

                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void readMessageClient()
        {
            while (true)
            {
                readList.Clear();
                for (int i = 0; i < acceptList.Count; i++)
                    readList.Add((Socket)acceptList[i]);

                if (readList.Count > 0)
                {
                    Socket.Select(readList, null, null, 1000);
                    for (int i = 0; i < readList.Count; i++)
                    {
                        byte[] bytes = new byte[((Socket)readList[i]).Available];
                        ((Socket)readList[i]).Receive(bytes, SocketFlags.None);
                        Console.WriteLine(Encoding.UTF8.GetString(bytes));
                    }
                }
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            server myserver = new server();
            myserver.start();
        }

    }
}
