using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form2 : Form
    {
        public Socket clientsocket;
        public Thread DataReceived = null;
        private string content = " helloooo";
        public Form2(Socket socket)
        {
            this.clientsocket = socket;
            InitializeComponent();
            //richTextBox1.AppendText(Environment.NewLine + DateTime.Today + content);
            try
	        {
		        DataReceived = new Thread(new ThreadStart(CheckData));
                DataReceived.IsBackground = true;
	 	        DataReceived.Start();
	        }
            catch (Exception E)
            {
                MessageBox.Show("Erreur démarrage Thread Données reçues du serveur" + E.Message);
            }
        }

        private void SendMessage(object sender, EventArgs e)
        {
            richTextBox1.AppendText(Environment.NewLine + DateTime.Today + content);
            if(clientsocket == null ||  !clientsocket.Connected)
            {
                MessageBox.Show("Vous n'êtes pas connecté");
            }
            try
            {
                if(textBox1.Text!="")
                {
                    string txt = textBox1.Text;
                    byte[] msg = Encoding.UTF8.GetBytes(txt);
                    clientsocket.Send(msg, SocketFlags.None);
                }
            }
            catch(Exception E)
            {
                MessageBox.Show("Envoi du message:" + E.Message);
            }
        }

        private void CheckData()
        {
            try
            {
                while(true)
                {
                    if(clientsocket.Connected)
                    {
                        // si le socket a des données à lire
                        if(true)
                        {
                            string msgrecu = null;
                            
                            while(true)
                            {
                                try
                                {
                                    byte[] msg = new Byte[clientsocket.Available];
                                    // reception du message
                                    clientsocket.Receive(msg, 0, clientsocket.Available, SocketFlags.None);
                                    msgrecu = System.Text.Encoding.UTF8.GetString(msg).Trim();
                                    content += msgrecu;
                                }
                                catch(SocketException E)
                                {
                                    MessageBox.Show("Check data" + E.Message);
                                }
                                try
                                {
                                    richTextBox1.Invoke(new UpdateTextCallback(this.UpdateText), new object[]{"Hello"});
                                    Console.WriteLine("Check Data");
                                    this.BringToFront();
                                }
                                catch(Exception E)
                                {
                                    MessageBox.Show(E.Message);
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(10);
            }
            catch
            {
                Thread.ResetAbort();
            }

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Rtf.Length;
            richTextBox1.Focus();
        }

        private void UpdateText(string text)
        {
            richTextBox1.AppendText(Environment.NewLine + DateTime.Today + content);

        }

        public delegate void UpdateTextCallback(string text);
    }
}
