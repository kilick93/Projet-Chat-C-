using CustomLibrary;
using Newtonsoft.Json;
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
        List<string> PseudoList { get; set; }
        public msg messagerecu = new msg();

        public server()
        {
            this.PseudoList = new List<string>();
        }

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
                        
                        try
                        {
                            
                            message = new byte[((Socket)readList[i]).Available];
                            ((Socket)readList[i]).Receive(message, SocketFlags.None);
                            Console.WriteLine(Encoding.UTF8.GetString(message));
                            messagerecu = JsonConvert.DeserializeObject<msg>(Encoding.UTF8.GetString(message));
                            
                            if (messagerecu.type == 1)
                            {
                                
                                if (!checkPseudo(messagerecu.pseudo, ((Socket)readList[i])))
                                {
                                    //break;
                                }
                            }
                            if (messagerecu.type == 5)
                            {
                                PseudoList.Remove(messagerecu.pseudo);
                                ((Socket)readList[i]).Shutdown(SocketShutdown.Both);
                                ((Socket)readList[i]).Close();
                                acceptList.Remove(((Socket)readList[i]));
                                Console.WriteLine("Deconnexion Client");
                            }
                        }
                        catch(SocketException e)
                        {
                            PseudoList.Remove(messagerecu.pseudo);
                            Console.WriteLine(e.Message);
                            ((Socket)acceptList[i]).Close();
                            acceptList.Remove(((Socket)acceptList[i]));
                        }
                        
                        
                        // Création d'un nouveau thread qui va renvoyer ce message à tous les clients
                        //Thread forwardall = new Thread(new ThreadStart(forward,messagerecu));
                        Thread forwardall = new Thread(() => forward(messagerecu));
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
        public void forward(msg messagereceived)
        {
            for (int i = 0; i < acceptList.Count; i++)
            {
                if (((Socket)acceptList[i]).Connected)
                {
                    try
                    {
                        messagereceived = JsonConvert.DeserializeObject<msg>(Encoding.UTF8.GetString(message));
                        if(messagereceived.type==2 || messagereceived.type==6)
                        {
                            ((Socket)acceptList[i]).Send(message, SocketFlags.None);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }
        private bool checkPseudo(string pseudo, Socket server)
        {
            try
            {
                
                if (PseudoList.Contains(pseudo))
                {
                    Console.WriteLine("Deconnexion du client");
                    msg deconnexion = new msg();
                    deconnexion.type = 5;
                    string output = JsonConvert.SerializeObject(deconnexion);
                    Console.WriteLine(output);
                    byte[] msg = Encoding.UTF8.GetBytes(output);
                    try
                    {

                        ((Socket)acceptList[acceptList.IndexOf(server)]).Send(msg);
                    }
                    catch
                    {
                    }
                    //Le pseudo est déjà pris, on refuse la connexion.
                    ((Socket)acceptList[acceptList.IndexOf(server)]).Shutdown(SocketShutdown.Both);
                    ((Socket)acceptList[acceptList.IndexOf(server)]).Close();
                    acceptList.Remove(server);
                    Console.WriteLine("Pseudo déjà pris");
                    return false;
                }
                else
                {
                    msg test = new msg();
                    test.type = 2;
                    //test.texte = pseudo + " s'est connecté";
                    string sortie = JsonConvert.SerializeObject(test);
                    byte[] test2 = Encoding.UTF8.GetBytes(sortie);
                    ((Socket)acceptList[acceptList.IndexOf(server)]).Send(test2);
                    //forward(test);
                    PseudoList.Add(pseudo);
                    return true;
                }
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
                return false;
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
