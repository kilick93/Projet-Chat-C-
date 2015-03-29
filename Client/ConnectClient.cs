using CustomLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class ConnectClient : Form
    {
        public string Pseudo = null;
        public Socket server = null;
        public msg mymessage;
        public ConnectClient()
        {
            InitializeComponent();
            mymessage = new msg();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        // Connexion au serveur après avoir renseigné les 3 champs : IP, Port, Pseudo
        private void btn_connect_Click(object sender, EventArgs e)
        {

            if (text_Pseudo.Text == "")
            {
                MessageBox.Show("Le pseudo ne peut être null");
                return;
            }
            if (text_IP.Text == "")
            {
                MessageBox.Show("L'adresse du serveur ne peut tre null");
                return;
            }
            if (text_Port.Text == "")
            {
                MessageBox.Show("Le port ne peut etre null");
                return;
            }

            Pseudo = text_Pseudo.Text;
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(text_IP.Text), int.Parse(text_Port.Text));
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                server.Connect(ipep);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Serveur indisponible");
            }
            if (server.Connected)
            {
                SendMessage(Pseudo,1);
            }

            while (server.Connected)
            {
                if (server.Available > 0)
                {
                    string checkconnexion = null;
                    byte[] msg = new Byte[server.Available];
                    // reception du message
                    server.Receive(msg, 0, server.Available, SocketFlags.None);
                    checkconnexion = System.Text.Encoding.UTF8.GetString(msg).Trim();
                    msg messagerecu = JsonConvert.DeserializeObject<msg>(checkconnexion);
                    if (messagerecu.type!=5)
                    {
                        Console.WriteLine("Creation Form 2");
                        ChatClient form2 = new ChatClient(server, Pseudo);
                        //SendMessage(Pseudo + " s'est connecté",6);
                        if (form2.ShowDialog() == DialogResult.OK)
                        {
                            this.Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Connexion impossible, Pseudo deja pris");
                        return;
                    }


                }
                else
                {
                    //Console.WriteLine("No Data Connexion");
                }
            }

  
        }

        void SendMessage(string message,int type)
        {
            mymessage.texte = message;
            mymessage.pseudo = Pseudo;
            mymessage.type = type; // Message de connexion
            string output = JsonConvert.SerializeObject(mymessage);
            byte[] msg = Encoding.UTF8.GetBytes(output);
            server.Send(msg, SocketFlags.None);
        }

    }
}
