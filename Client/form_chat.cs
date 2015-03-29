using CustomLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class form_chat : Form
    {
        public Socket clientsocket;
        public Thread DataReceived = null;
        public Thread UserListReceived = null;
        private string content = null;
        public msg mymessage;
        public msg connexion;
        public msg deconnexion;
        public string pseudo = null;
        private int canal;
        List<int> channels;
        List<String> PseudoList = new List<string>();
        public form_chat(Socket socket, string name)
        {
            this.canal = 1;
            this.clientsocket = socket;
            this.pseudo=name;
            InitializeComponent();
            mymessage = new msg();
            connexion = new msg();
            deconnexion = new msg();
            deconnexion.texte = pseudo + " s'est deconnecté";
            deconnexion.pseudo = pseudo;
            deconnexion.canal = canal;
            connexion.pseudo=pseudo;
            connexion.canal=canal;
            connexion.texte=pseudo+" s'est connecté";
            channels = new List<int>();
            channels.Add(1);
            channels.Add(2);
            channels.Add(3);
            channels.Add(4);
            channels.Add(5);
            channels.Add(6);
            this.canal = 1;
            this.Label_Canal.Text = 1.ToString();
            refreshChannelView();
            SendMessage(connexion, 6, canal);

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
            if(clientsocket == null ||  !clientsocket.Connected)
            {
                MessageBox.Show("Vous n'êtes pas connecté");
            }
            else
            {
                try
                {
                    //On vérifie qu'un message a été écrit
                    if (tbChat.Text != "")
                    {
                        mymessage.texte = tbChat.Text;
                        mymessage.pseudo = pseudo;
                        mymessage.canal = canal;
                        mymessage.type = 2;
                        string output = JsonConvert.SerializeObject(mymessage);
                        byte[] msg = Encoding.UTF8.GetBytes(output);
                        clientsocket.Send(msg, SocketFlags.None);

                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show("Envoi du message:" + E.Message);
                }
                tbChat.Text = "";
            }
            
        }
        // Thread qui vérifie en permanence si des données ont été envoyés par le serveur
        private void CheckData()
        {
            try
            
            {
            
                while(true)
                {
                    if(clientsocket.Connected)
                    {
                    
                        // si le socket a des données à lire
                        if(clientsocket.Available>0)
                        {
                           
                            string msgrecu = null;
                            content = null;
                            msg messagerecu = new msg();
                            // Tant qu'il y a des données à lire
                            while(clientsocket.Available>0)
                            {

                                try
                                {
                            
                                    byte[] msg = new Byte[clientsocket.Available];
                                    // reception du message
                                    clientsocket.Receive(msg, 0, clientsocket.Available, SocketFlags.None);
                                    msgrecu = System.Text.Encoding.UTF8.GetString(msg).Trim();
                                    messagerecu = JsonConvert.DeserializeObject<msg>(msgrecu); 
                                    if (messagerecu.canal == canal)
                                    {

                                        if (messagerecu.type == 2)
                                        {
                                            content += messagerecu.pseudo + " a écrit : " + messagerecu.texte;
                                        }
                                        else if (messagerecu.type == 6 || messagerecu.type == 5)
                                        {
                                            content += messagerecu.texte;
                                            PseudoList.Clear();
                                            PseudoList = messagerecu.pseudolist;
                                            foreach(string pseudo in PseudoList)
                                            {
                                                Console.WriteLine(" un pseudo wow : " + pseudo);
                                            }
                                            if (listViewUsers.InvokeRequired)
                                            {
                                                refreshUserViewInvokeHandler MethodeDelegate = new refreshUserViewInvokeHandler(refreshUserView);
                                                IAsyncResult iar = this.BeginInvoke(MethodeDelegate);
                                                this.EndInvoke(iar);
                                            }
                                            else
                                            {
                                                refreshUserView();
                                            }
                                            
                                        }

                                    }
                                    else
                                    {
                                        // Do Nothing
                                    }
                                    
                                                                   
                                }
                                catch(SocketException E)
                                {
                                    MessageBox.Show("Check data" + E.Message);
                                }
                            }
                            try
                            {
                                if (messagerecu.canal == canal)
                                {

                                    if (rtbChat.InvokeRequired)
                                    {
                                        TextBoxInvokeHandler MethodeDelegate = new TextBoxInvokeHandler(FonctionTextBox);
                                        IAsyncResult iar = this.BeginInvoke(MethodeDelegate, content);
                                        this.EndInvoke(iar);
                                    }
                                    else
                                    {
                                        FonctionTextBox(content);
                                    }
                                }
                                else
                                {
                                    // do nothing
                                }
                            }
                            catch(Exception E)
                            {
 
                                MessageBox.Show(E.Message);
                            }
                            
                        }
                    }
                Thread.Sleep(10);
                }
                
            }
            catch(ThreadStateException e)
            {
                Console.WriteLine(e.Message);
                Thread.ResetAbort();
            }

        }

        

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            rtbChat.SelectionStart = rtbChat.Rtf.Length;
            rtbChat.Focus();
        }
        private delegate void TextBoxInvokeHandler(string msg);
        // Mise à jour de la fenêtre de chat
        private void FonctionTextBox(string msg)
        {
            this.rtbChat.AppendText(msg + "\n");
        }

        private void ClearTextBox()
        {
            this.rtbChat.ResetText();
        }
        // Envoi d'une image
        private void btnSendPic_Click(object sender, EventArgs e)
        {
            ofdPictureChooser.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if(ofdPictureChooser.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(ofdPictureChooser.FileName);
                Console.WriteLine(ofdPictureChooser.FileName);
                mymessage.image = ImageToByte(bmp);

            }
        }
        // Convertir une image en byte[]
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SendMessage(deconnexion,6,canal);
            SendMessage(deconnexion,5,canal);
            this.DialogResult = DialogResult.OK;
        }
        // Envoi d'un message au serveur
        void SendMessage(msg message, int type,int canal)
        {
            
            message.pseudo = pseudo;
            message.type = type;
            message.canal = canal;
            string output = JsonConvert.SerializeObject(message);
            byte[] msg = Encoding.UTF8.GetBytes(output);
            clientsocket.Send(msg, SocketFlags.None);
        }

        
        private void refreshChannelView()
        {
            //on efface tous les items déjà présents dans la listview
            listViewChannel.Items.Clear();
            //on prend chaque canal dans la liste channels
            foreach (int canal in channels)
            {
                ListViewItem item = new ListViewItem();
                item.Text = canal.ToString();
                listViewChannel.Items.Add(item);
            }
        }
        private delegate void refreshUserViewInvokeHandler();
        private void refreshUserView()
        {
            //on efface tous les items déjà présents dans la listview
            listViewUsers.Items.Clear();
            //on prend chaque pseudo dans la PseudoList
            foreach (string pseudo in PseudoList)
            {
                //on récupère le pseudo et l'ajoute dans la listviewusers
                ListViewItem item = new ListViewItem();
                item.Text = pseudo;
                listViewUsers.Items.Add(item);
            }
        }
        // Changement de canal
        private void listViewChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewChannel.SelectedItems.Count == 1)
            {
                if(listViewChannel.SelectedIndices[0]+1 != canal)
                {
                    ClearTextBox();
                    //envoi au serveur d'un message de déconnexion du canal
                    SendMessage(deconnexion, 6, canal);
                    this.canal = listViewChannel.SelectedIndices[0] + 1;
                    this.Label_Canal.Text = (listViewChannel.SelectedIndices[0] + 1).ToString();
                    //envoi au serveur d'un message de connexion du canal
                    SendMessage(connexion, 6, canal);
                }
            }
        }
        //Appuyer sur Entrée dans la textbox envoie le message
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Touche Escape
            if (keyData == Keys.Enter)
            {
                if (clientsocket == null || !clientsocket.Connected)
                {
                    MessageBox.Show("Vous n'êtes pas connecté");
                }
                else
                {
                    try
                    {
                        if (tbChat.Text != "")
                        {
                            mymessage.texte = tbChat.Text;
                            mymessage.pseudo = pseudo;
                            mymessage.canal = canal;
                            mymessage.type = 2;
                            string output = JsonConvert.SerializeObject(mymessage);
                            byte[] txt = Encoding.UTF8.GetBytes(output);
                            clientsocket.Send(txt, SocketFlags.None);

                        }
                    }
                    catch (Exception E)
                    {
                        MessageBox.Show("Envoi du message:" + E.Message);
                    }
                    tbChat.Text = "";
                }
                return true;
            }
            else
                return base.ProcessCmdKey(ref msg, keyData);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }


    }
}
