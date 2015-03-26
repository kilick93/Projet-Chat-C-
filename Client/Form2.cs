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
    public partial class Form2 : Form
    {
        public Socket clientsocket;
        public Thread DataReceived = null;
        private string content = null;
        public msg mymessage;
        public Form2(Socket socket)
        {
            this.clientsocket = socket;
            InitializeComponent();
            mymessage = new msg();
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
                    mymessage.texte = textBox1.Text;
                    mymessage.pseudo = "jeanpaul";
                    string output = JsonConvert.SerializeObject(mymessage);
                    byte[] msg = Encoding.UTF8.GetBytes(output);
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
                        if(clientsocket.Available>0)
                        {
                           
                            string msgrecu = null;
                            content = null;
                            while(clientsocket.Available>0)
                            {
                          
                                try
                                {
                            
                                    byte[] msg = new Byte[clientsocket.Available];
                                    // reception du message
                                    clientsocket.Receive(msg, 0, clientsocket.Available, SocketFlags.None);
                                    msgrecu = System.Text.Encoding.UTF8.GetString(msg).Trim();
                                    msg messagerecu = JsonConvert.DeserializeObject<msg>(msgrecu);
                                    content += messagerecu.pseudo + " a écrit : " + messagerecu.texte;
                                    /*try
                                    {
                                        Clipboard.SetDataObject(messagerecu.image);
                                        DataFormats.Format myformat = DataFormats.GetFormat(DataFormats.Bitmap);
                                        if(richTextBox1.CanPaste(myformat))
                                        {
                                            richTextBox1.Paste(myformat);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Can't paste");
                                        }
                                    }
                                    catch(Exception e)
                                    {
                                        Console.WriteLine(e.Message);
                                    }*/
                                    
                                }
                                catch(SocketException E)
                                {
                                    MessageBox.Show("Check data" + E.Message);
                                }
                            }
                            try
                            {
                 
                                /*MethodInvoker action = delegate
                                { richTextBox1.AppendText(Environment.NewLine + DateTime.Today + content); };
                                richTextBox1.Invoke(action);*/
                                if (richTextBox1.InvokeRequired)
                                {
                                    TextBoxInvokeHandler MethodeDelegate = new TextBoxInvokeHandler(FonctionTextBox);
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

        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }


    }
}
