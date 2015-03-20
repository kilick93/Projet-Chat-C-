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
        // acceptList contient la liste de tous les clients connectés
        public ArrayList acceptList = new ArrayList();
        public ArrayList readList = new ArrayList();
        byte[] message;

        // démarrage du serveur
        public void start()
        {
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Thread readclient = new Thread(new ThreadStart(readMessageClient));
            // Thread qui scan en permanence si un client a envoyé un message
            readclient.Start();
            try
            {
                // On bind la socket au port 8000 et jusqu'à 100 clients peuvent se connecter simultanément
                newsock.Bind(localEndPoint);
                newsock.Listen(100);
                while (true)
                {
                    // Dès qu'un client se connecte, on l'accepte et on l'ajoute à l'acceptList
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

        // Récupération des messages envoyés par le client et forward à tous les autres clients
        public void readMessageClient()
        {
            while (true)
            {
                readList.Clear();
                for (int i = 0; i < acceptList.Count; i++)
                    readList.Add((Socket)acceptList[i]);

                if (readList.Count > 0)
                {
                    // Socket.Select permet de ne garder ques les sockets qui ont un message à envoyer
                    Socket.Select(readList, null, null, 1000);
                    for (int i = 0; i < readList.Count; i++)
                    {
                        // Enregistrement du message
                        message = new byte[((Socket)readList[i]).Available];
                        ((Socket)readList[i]).Receive(message, SocketFlags.None);
                        Console.WriteLine(Encoding.UTF8.GetString(message));
                        // Création d'un nouveau thread qui va renvoyer ce message à tous les clients
                        Thread forwardall = new Thread(new ThreadStart(forward));
                        // Démarrage du thread
                        forwardall.Start();
                        // Dès que le thread a terminé sa tâche, le thread est fermé
                        forwardall.Join();
                        // Ajout d'un sleep pour temporiser le thread et éviter que le CPU ne soit surchargé
                        Thread.Sleep(10);
                    }
                }
            }
        }
        // Envoi du message à tous les clients connectés
        public void forward()
        {
            for (int i = 0; i < acceptList.Count; i++)
            {
                if (((Socket)acceptList[i]).Connected)
                {
                    try
                    {
                        ((Socket)acceptList[i]).Send(message, SocketFlags.None);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("----------Initialisation du serveur----------");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("---------------------------------------------");
            server myserver = new server();
            myserver.start();
        }

    }
}
