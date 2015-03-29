namespace Client
{
    partial class form_chat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form_chat));
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.lbCurrentChannel = new System.Windows.Forms.Label();
            this.lbConnectedUsers = new System.Windows.Forms.Label();
            this.btnSendPic = new System.Windows.Forms.Button();
            this.ofdPictureChooser = new System.Windows.Forms.OpenFileDialog();
            this.listViewChannel = new System.Windows.Forms.ListView();
            this.listViewUsers = new System.Windows.Forms.ListView();
            this.Label_Canal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtbChat
            // 
            this.rtbChat.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.rtbChat.Location = new System.Drawing.Point(153, 40);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(459, 305);
            this.rtbChat.TabIndex = 0;
            this.rtbChat.Text = "";
            this.rtbChat.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            this.rtbChat.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // tbChat
            // 
            this.tbChat.Location = new System.Drawing.Point(153, 365);
            this.tbChat.Name = "tbChat";
            this.tbChat.Size = new System.Drawing.Size(351, 20);
            this.tbChat.TabIndex = 1;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(510, 365);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(102, 20);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.SendMessage);
            // 
            // lbCurrentChannel
            // 
            this.lbCurrentChannel.AutoSize = true;
            this.lbCurrentChannel.Location = new System.Drawing.Point(14, 13);
            this.lbCurrentChannel.Name = "lbCurrentChannel";
            this.lbCurrentChannel.Size = new System.Drawing.Size(83, 13);
            this.lbCurrentChannel.TabIndex = 5;
            this.lbCurrentChannel.Text = "Current Channel";
            // 
            // lbConnectedUsers
            // 
            this.lbConnectedUsers.AutoSize = true;
            this.lbConnectedUsers.Location = new System.Drawing.Point(629, 13);
            this.lbConnectedUsers.Name = "lbConnectedUsers";
            this.lbConnectedUsers.Size = new System.Drawing.Size(152, 13);
            this.lbConnectedUsers.TabIndex = 6;
            this.lbConnectedUsers.Text = "Connected users on the server";
            // 
            // btnSendPic
            // 
            this.btnSendPic.Enabled = false;
            this.btnSendPic.Location = new System.Drawing.Point(632, 362);
            this.btnSendPic.Name = "btnSendPic";
            this.btnSendPic.Size = new System.Drawing.Size(131, 23);
            this.btnSendPic.TabIndex = 7;
            this.btnSendPic.Text = "Join a picture";
            this.btnSendPic.UseVisualStyleBackColor = true;
            this.btnSendPic.Click += new System.EventHandler(this.btnSendPic_Click);
            // 
            // ofdPictureChooser
            // 
            this.ofdPictureChooser.FileName = "Image Files|*.jpg;*.jpeg;*.png";
            this.ofdPictureChooser.Title = "Choose a picture";
            // 
            // listViewChannel
            // 
            this.listViewChannel.Location = new System.Drawing.Point(17, 40);
            this.listViewChannel.Name = "listViewChannel";
            this.listViewChannel.Size = new System.Drawing.Size(102, 345);
            this.listViewChannel.TabIndex = 8;
            this.listViewChannel.UseCompatibleStateImageBehavior = false;
            this.listViewChannel.SelectedIndexChanged += new System.EventHandler(this.listViewChannel_SelectedIndexChanged);
            // 
            // listViewUsers
            // 
            this.listViewUsers.Location = new System.Drawing.Point(633, 40);
            this.listViewUsers.Name = "listViewUsers";
            this.listViewUsers.Size = new System.Drawing.Size(129, 304);
            this.listViewUsers.TabIndex = 9;
            this.listViewUsers.UseCompatibleStateImageBehavior = false;
            // 
            // Label_Canal
            // 
            this.Label_Canal.AutoSize = true;
            this.Label_Canal.Location = new System.Drawing.Point(103, 13);
            this.Label_Canal.Name = "Label_Canal";
            this.Label_Canal.Size = new System.Drawing.Size(0, 13);
            this.Label_Canal.TabIndex = 10;
            // 
            // form_chat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 397);
            this.Controls.Add(this.Label_Canal);
            this.Controls.Add(this.listViewUsers);
            this.Controls.Add(this.listViewChannel);
            this.Controls.Add(this.btnSendPic);
            this.Controls.Add(this.lbConnectedUsers);
            this.Controls.Add(this.lbCurrentChannel);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.rtbChat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "form_chat";
            this.Text = "Chat ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Label lbCurrentChannel;
        private System.Windows.Forms.Label lbConnectedUsers;
        private System.Windows.Forms.Button btnSendPic;
        private System.Windows.Forms.OpenFileDialog ofdPictureChooser;
        private System.Windows.Forms.ListView listViewChannel;
        private System.Windows.Forms.ListView listViewUsers;
        private System.Windows.Forms.Label Label_Canal;
    }
}