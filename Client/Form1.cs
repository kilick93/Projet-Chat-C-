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
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipep);
             * */
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            /*
            // Créer un nouveau client TCP
            TcpClient TcpServer = new TcpClient();
            //Connexion à l'adresse et au port indiqués
            TcpServer.Connect(text_IP.Text, int.Parse(text_Port.Text));
            //Fermeture de la connexion
            TcpServer.Close();
             * */
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(text_IP.Text), int.Parse(text_Port.Text));
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(ipep);
            Form2 form2 = new Form2(server);
            if(form2.ShowDialog()==DialogResult.OK)
            {

            }

            
        }

    }
}
