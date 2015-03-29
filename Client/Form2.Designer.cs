namespace Client
{
    partial class Form2
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSendPic = new System.Windows.Forms.Button();
            this.ofdPictureChooser = new System.Windows.Forms.OpenFileDialog();
            this.listViewChannel = new System.Windows.Forms.ListView();
            this.listViewUsers = new System.Windows.Forms.ListView();
            this.Label_Canal = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(153, 40);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(459, 305);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(153, 365);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(351, 20);
            this.textBox1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(510, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(102, 20);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send Message";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SendMessage);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current Channel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(629, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Connected Users";
            // 
            // btnSendPic
            // 
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
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 397);
            this.Controls.Add(this.Label_Canal);
            this.Controls.Add(this.listViewUsers);
            this.Controls.Add(this.listViewChannel);
            this.Controls.Add(this.btnSendPic);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form2_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSendPic;
        private System.Windows.Forms.OpenFileDialog ofdPictureChooser;
        private System.Windows.Forms.ListView listViewChannel;
        private System.Windows.Forms.ListView listViewUsers;
        private System.Windows.Forms.Label Label_Canal;
    }
}