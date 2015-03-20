﻿using System;
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
            //richTextBox1.AppendText(Environment.NewLine + DateTime.Today + content);
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
                Console.WriteLine("Check Data1");
                while(true)
                {
                    if(clientsocket.Connected)
                    {
                        Console.WriteLine("Check Data2");
                        // si le socket a des données à lire
                        if(true)
                        {
                            Console.WriteLine("Check Data3");
                            string msgrecu = null;
                            /*
                            while(true)
                            {
                                Console.WriteLine("Check Data4");
                                try
                                {
                                    Console.WriteLine("Check Data5");
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
                            }*/
                            try
                            {
                                Console.WriteLine("Check Data6");
                                /*MethodInvoker action = delegate
                                { richTextBox1.AppendText(Environment.NewLine + DateTime.Today + content); };
                                richTextBox1.Invoke(action);*/
                                if (richTextBox1.InvokeRequired)
                                {
                                    TextBoxInvokeHandler MethodeDelegate = new TextBoxInvokeHandler(FonctionTextBox);
                                    this.Invoke(MethodeDelegate,content);
                                    IAsyncResult iar  = this.BeginInvoke(MethodeDelegate,content);
                                    this.EndInvoke(iar);
                                }
                                else
                                {
                                   FonctionTextBox(content);
                                }

                            }
                            catch(Exception E)
                            {
                                Console.WriteLine("Check Data7");
                                MessageBox.Show(E.Message);
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
        private delegate void TextBoxInvokeHandler(string msg);

        private void FonctionTextBox(string msg)
        {
            this.richTextBox1.AppendText(msg + "\n");
        }


    }
}
