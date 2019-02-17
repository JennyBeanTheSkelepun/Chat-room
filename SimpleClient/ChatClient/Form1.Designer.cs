
namespace ChatClient
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.chatWindow = new System.Windows.Forms.RichTextBox();
            this.gameTabControl = new System.Windows.Forms.TabControl();
            this.tabTicTacToe = new System.Windows.Forms.TabPage();
            this.XO_ExitGame = new System.Windows.Forms.Button();
            this.XO_OutputBox = new System.Windows.Forms.TextBox();
            this.XO_8 = new System.Windows.Forms.Button();
            this.XO_7 = new System.Windows.Forms.Button();
            this.XO_6 = new System.Windows.Forms.Button();
            this.XO_5 = new System.Windows.Forms.Button();
            this.XO_4 = new System.Windows.Forms.Button();
            this.XO_3 = new System.Windows.Forms.Button();
            this.XO_2 = new System.Windows.Forms.Button();
            this.XO_1 = new System.Windows.Forms.Button();
            this.XO_0 = new System.Windows.Forms.Button();
            this.XO_Label1 = new System.Windows.Forms.Label();
            this.XO_Player2Button = new System.Windows.Forms.Button();
            this.XO_Player1Button = new System.Windows.Forms.Button();
            this.UDP_PingButton = new System.Windows.Forms.Button();
            this.SecretTab = new System.Windows.Forms.TabPage();
            this.OddButton = new System.Windows.Forms.Button();
            this.ConnectionTab = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.usernameButton = new System.Windows.Forms.Button();
            this.IPaddressBox = new System.Windows.Forms.TextBox();
            this.usernameBox = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.TabControlSettings = new System.Windows.Forms.TabControl();
            this.gameTabControl.SuspendLayout();
            this.tabTicTacToe.SuspendLayout();
            this.SecretTab.SuspendLayout();
            this.ConnectionTab.SuspendLayout();
            this.TabControlSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chat Client";
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(12, 417);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(244, 20);
            this.messageBox.TabIndex = 1;
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(262, 415);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.SendButtonClick);
            // 
            // chatWindow
            // 
            this.chatWindow.Location = new System.Drawing.Point(12, 31);
            this.chatWindow.Name = "chatWindow";
            this.chatWindow.Size = new System.Drawing.Size(325, 381);
            this.chatWindow.TabIndex = 3;
            this.chatWindow.Text = "";
            // 
            // gameTabControl
            // 
            this.gameTabControl.Controls.Add(this.tabTicTacToe);
            this.gameTabControl.Location = new System.Drawing.Point(343, 156);
            this.gameTabControl.Name = "gameTabControl";
            this.gameTabControl.SelectedIndex = 0;
            this.gameTabControl.Size = new System.Drawing.Size(301, 256);
            this.gameTabControl.TabIndex = 16;
            // 
            // tabTicTacToe
            // 
            this.tabTicTacToe.Controls.Add(this.XO_ExitGame);
            this.tabTicTacToe.Controls.Add(this.XO_OutputBox);
            this.tabTicTacToe.Controls.Add(this.XO_8);
            this.tabTicTacToe.Controls.Add(this.XO_7);
            this.tabTicTacToe.Controls.Add(this.XO_6);
            this.tabTicTacToe.Controls.Add(this.XO_5);
            this.tabTicTacToe.Controls.Add(this.XO_4);
            this.tabTicTacToe.Controls.Add(this.XO_3);
            this.tabTicTacToe.Controls.Add(this.XO_2);
            this.tabTicTacToe.Controls.Add(this.XO_1);
            this.tabTicTacToe.Controls.Add(this.XO_0);
            this.tabTicTacToe.Controls.Add(this.XO_Label1);
            this.tabTicTacToe.Controls.Add(this.XO_Player2Button);
            this.tabTicTacToe.Controls.Add(this.XO_Player1Button);
            this.tabTicTacToe.Location = new System.Drawing.Point(4, 22);
            this.tabTicTacToe.Name = "tabTicTacToe";
            this.tabTicTacToe.Padding = new System.Windows.Forms.Padding(3);
            this.tabTicTacToe.Size = new System.Drawing.Size(293, 230);
            this.tabTicTacToe.TabIndex = 0;
            this.tabTicTacToe.Text = "Naughts & Crosses";
            this.tabTicTacToe.UseVisualStyleBackColor = true;
            // 
            // XO_ExitGame
            // 
            this.XO_ExitGame.Location = new System.Drawing.Point(169, 22);
            this.XO_ExitGame.Name = "XO_ExitGame";
            this.XO_ExitGame.Size = new System.Drawing.Size(75, 23);
            this.XO_ExitGame.TabIndex = 37;
            this.XO_ExitGame.Text = "Exit Game";
            this.XO_ExitGame.UseVisualStyleBackColor = true;
            this.XO_ExitGame.Click += new System.EventHandler(this.XO_ExitButton_Click);
            // 
            // XO_OutputBox
            // 
            this.XO_OutputBox.Location = new System.Drawing.Point(16, 197);
            this.XO_OutputBox.Name = "XO_OutputBox";
            this.XO_OutputBox.Size = new System.Drawing.Size(129, 20);
            this.XO_OutputBox.TabIndex = 36;
            // 
            // XO_8
            // 
            this.XO_8.Location = new System.Drawing.Point(106, 152);
            this.XO_8.Name = "XO_8";
            this.XO_8.Size = new System.Drawing.Size(39, 38);
            this.XO_8.TabIndex = 35;
            this.XO_8.UseVisualStyleBackColor = true;
            this.XO_8.Click += new System.EventHandler(this.XO_8_Click);
            // 
            // XO_7
            // 
            this.XO_7.Location = new System.Drawing.Point(61, 152);
            this.XO_7.Name = "XO_7";
            this.XO_7.Size = new System.Drawing.Size(39, 38);
            this.XO_7.TabIndex = 34;
            this.XO_7.UseVisualStyleBackColor = true;
            this.XO_7.Click += new System.EventHandler(this.XO_7_Click);
            // 
            // XO_6
            // 
            this.XO_6.Location = new System.Drawing.Point(16, 152);
            this.XO_6.Name = "XO_6";
            this.XO_6.Size = new System.Drawing.Size(39, 38);
            this.XO_6.TabIndex = 33;
            this.XO_6.UseVisualStyleBackColor = true;
            this.XO_6.Click += new System.EventHandler(this.XO_6_Click);
            // 
            // XO_5
            // 
            this.XO_5.Location = new System.Drawing.Point(106, 108);
            this.XO_5.Name = "XO_5";
            this.XO_5.Size = new System.Drawing.Size(39, 38);
            this.XO_5.TabIndex = 32;
            this.XO_5.UseVisualStyleBackColor = true;
            this.XO_5.Click += new System.EventHandler(this.XO_5_Click);
            // 
            // XO_4
            // 
            this.XO_4.Location = new System.Drawing.Point(61, 108);
            this.XO_4.Name = "XO_4";
            this.XO_4.Size = new System.Drawing.Size(39, 38);
            this.XO_4.TabIndex = 31;
            this.XO_4.UseVisualStyleBackColor = true;
            this.XO_4.Click += new System.EventHandler(this.XO_4_Click);
            // 
            // XO_3
            // 
            this.XO_3.Location = new System.Drawing.Point(16, 108);
            this.XO_3.Name = "XO_3";
            this.XO_3.Size = new System.Drawing.Size(39, 38);
            this.XO_3.TabIndex = 30;
            this.XO_3.UseVisualStyleBackColor = true;
            this.XO_3.Click += new System.EventHandler(this.XO_3_Click);
            // 
            // XO_2
            // 
            this.XO_2.Location = new System.Drawing.Point(106, 64);
            this.XO_2.Name = "XO_2";
            this.XO_2.Size = new System.Drawing.Size(39, 38);
            this.XO_2.TabIndex = 29;
            this.XO_2.UseVisualStyleBackColor = true;
            this.XO_2.Click += new System.EventHandler(this.XO_2_Click);
            // 
            // XO_1
            // 
            this.XO_1.Location = new System.Drawing.Point(61, 64);
            this.XO_1.Name = "XO_1";
            this.XO_1.Size = new System.Drawing.Size(39, 38);
            this.XO_1.TabIndex = 28;
            this.XO_1.UseVisualStyleBackColor = true;
            this.XO_1.Click += new System.EventHandler(this.XO_1_Click);
            // 
            // XO_0
            // 
            this.XO_0.Location = new System.Drawing.Point(16, 64);
            this.XO_0.Name = "XO_0";
            this.XO_0.Size = new System.Drawing.Size(39, 38);
            this.XO_0.TabIndex = 27;
            this.XO_0.UseVisualStyleBackColor = true;
            this.XO_0.Click += new System.EventHandler(this.XO_0_Click);
            // 
            // XO_Label1
            // 
            this.XO_Label1.AutoSize = true;
            this.XO_Label1.Location = new System.Drawing.Point(6, 6);
            this.XO_Label1.Name = "XO_Label1";
            this.XO_Label1.Size = new System.Drawing.Size(132, 13);
            this.XO_Label1.TabIndex = 26;
            this.XO_Label1.Text = "Click one to join the game:";
            // 
            // XO_Player2Button
            // 
            this.XO_Player2Button.Location = new System.Drawing.Point(87, 22);
            this.XO_Player2Button.Name = "XO_Player2Button";
            this.XO_Player2Button.Size = new System.Drawing.Size(75, 23);
            this.XO_Player2Button.TabIndex = 25;
            this.XO_Player2Button.Text = "X - [Empty]";
            this.XO_Player2Button.UseVisualStyleBackColor = true;
            this.XO_Player2Button.Click += new System.EventHandler(this.XO_Player2Button_Click);
            // 
            // XO_Player1Button
            // 
            this.XO_Player1Button.Location = new System.Drawing.Point(6, 22);
            this.XO_Player1Button.Name = "XO_Player1Button";
            this.XO_Player1Button.Size = new System.Drawing.Size(75, 23);
            this.XO_Player1Button.TabIndex = 24;
            this.XO_Player1Button.Text = "O - [Empty]";
            this.XO_Player1Button.UseVisualStyleBackColor = true;
            this.XO_Player1Button.Click += new System.EventHandler(this.XO_Player1Button_Click);
            // 
            // UDP_PingButton
            // 
            this.UDP_PingButton.Location = new System.Drawing.Point(6, 6);
            this.UDP_PingButton.Name = "UDP_PingButton";
            this.UDP_PingButton.Size = new System.Drawing.Size(94, 23);
            this.UDP_PingButton.TabIndex = 0;
            this.UDP_PingButton.Text = "Ping Server";
            this.UDP_PingButton.UseVisualStyleBackColor = true;
            this.UDP_PingButton.Click += new System.EventHandler(this.UDP_PingButton_Click);
            // 
            // SecretTab
            // 
            this.SecretTab.Controls.Add(this.UDP_PingButton);
            this.SecretTab.Controls.Add(this.OddButton);
            this.SecretTab.Location = new System.Drawing.Point(4, 22);
            this.SecretTab.Name = "SecretTab";
            this.SecretTab.Padding = new System.Windows.Forms.Padding(3);
            this.SecretTab.Size = new System.Drawing.Size(297, 114);
            this.SecretTab.TabIndex = 2;
            this.SecretTab.Text = "UDP";
            this.SecretTab.UseVisualStyleBackColor = true;
            // 
            // OddButton
            // 
            this.OddButton.Cursor = System.Windows.Forms.Cursors.No;
            this.OddButton.Location = new System.Drawing.Point(6, 35);
            this.OddButton.Name = "OddButton";
            this.OddButton.Size = new System.Drawing.Size(94, 23);
            this.OddButton.TabIndex = 15;
            this.OddButton.Text = "Don\'t Push Me!";
            this.OddButton.UseVisualStyleBackColor = true;
            this.OddButton.Click += new System.EventHandler(this.OddButton_Click);
            // 
            // ConnectionTab
            // 
            this.ConnectionTab.Controls.Add(this.label7);
            this.ConnectionTab.Controls.Add(this.label6);
            this.ConnectionTab.Controls.Add(this.label5);
            this.ConnectionTab.Controls.Add(this.label3);
            this.ConnectionTab.Controls.Add(this.usernameButton);
            this.ConnectionTab.Controls.Add(this.IPaddressBox);
            this.ConnectionTab.Controls.Add(this.usernameBox);
            this.ConnectionTab.Controls.Add(this.portBox);
            this.ConnectionTab.Controls.Add(this.label2);
            this.ConnectionTab.Controls.Add(this.label4);
            this.ConnectionTab.Controls.Add(this.disconnectButton);
            this.ConnectionTab.Controls.Add(this.connectButton);
            this.ConnectionTab.Location = new System.Drawing.Point(4, 22);
            this.ConnectionTab.Name = "ConnectionTab";
            this.ConnectionTab.Padding = new System.Windows.Forms.Padding(3);
            this.ConnectionTab.Size = new System.Drawing.Size(297, 114);
            this.ConnectionTab.TabIndex = 0;
            this.ConnectionTab.Text = "Connection";
            this.ConnectionTab.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Username";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(128, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Port";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "IP Address";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 5;
            // 
            // usernameButton
            // 
            this.usernameButton.Location = new System.Drawing.Point(106, 84);
            this.usernameButton.Name = "usernameButton";
            this.usernameButton.Size = new System.Drawing.Size(88, 20);
            this.usernameButton.TabIndex = 13;
            this.usernameButton.Text = "Set Username";
            this.usernameButton.UseVisualStyleBackColor = true;
            this.usernameButton.Click += new System.EventHandler(this.UsernameButtonClick);
            // 
            // IPaddressBox
            // 
            this.IPaddressBox.Location = new System.Drawing.Point(6, 19);
            this.IPaddressBox.Name = "IPaddressBox";
            this.IPaddressBox.Size = new System.Drawing.Size(116, 20);
            this.IPaddressBox.TabIndex = 7;
            this.IPaddressBox.Text = "127.0.0.1";
            // 
            // usernameBox
            // 
            this.usernameBox.Location = new System.Drawing.Point(6, 84);
            this.usernameBox.Name = "usernameBox";
            this.usernameBox.Size = new System.Drawing.Size(94, 20);
            this.usernameBox.TabIndex = 12;
            this.usernameBox.Text = "User1";
            // 
            // portBox
            // 
            this.portBox.Location = new System.Drawing.Point(131, 19);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(63, 20);
            this.portBox.TabIndex = 8;
            this.portBox.Text = "4444";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 6;
            // 
            // disconnectButton
            // 
            this.disconnectButton.Location = new System.Drawing.Point(88, 45);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(76, 20);
            this.disconnectButton.TabIndex = 10;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Click += new System.EventHandler(this.DisconnectButtonClick);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(6, 45);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(76, 20);
            this.connectButton.TabIndex = 9;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // TabControlSettings
            // 
            this.TabControlSettings.Controls.Add(this.ConnectionTab);
            this.TabControlSettings.Controls.Add(this.SecretTab);
            this.TabControlSettings.Location = new System.Drawing.Point(343, 10);
            this.TabControlSettings.Name = "TabControlSettings";
            this.TabControlSettings.SelectedIndex = 0;
            this.TabControlSettings.Size = new System.Drawing.Size(305, 140);
            this.TabControlSettings.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 450);
            this.Controls.Add(this.gameTabControl);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.TabControlSettings);
            this.Controls.Add(this.chatWindow);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Chat Client";
            this.gameTabControl.ResumeLayout(false);
            this.tabTicTacToe.ResumeLayout(false);
            this.tabTicTacToe.PerformLayout();
            this.SecretTab.ResumeLayout(false);
            this.ConnectionTab.ResumeLayout(false);
            this.ConnectionTab.PerformLayout();
            this.TabControlSettings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox messageBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.RichTextBox chatWindow;
        private System.Windows.Forms.TabControl gameTabControl;
        private System.Windows.Forms.TabPage tabTicTacToe;
        private System.Windows.Forms.Label XO_Label1;
        private System.Windows.Forms.Button XO_Player2Button;
        private System.Windows.Forms.Button XO_Player1Button;
        private System.Windows.Forms.Button XO_8;
        private System.Windows.Forms.Button XO_7;
        private System.Windows.Forms.Button XO_6;
        private System.Windows.Forms.Button XO_5;
        private System.Windows.Forms.Button XO_4;
        private System.Windows.Forms.Button XO_3;
        private System.Windows.Forms.Button XO_2;
        private System.Windows.Forms.Button XO_1;
        private System.Windows.Forms.Button XO_0;
        private System.Windows.Forms.TextBox XO_OutputBox;
        private System.Windows.Forms.Button XO_ExitGame;
        private System.Windows.Forms.Button UDP_PingButton;
        private System.Windows.Forms.TabPage SecretTab;
        private System.Windows.Forms.Button OddButton;
        private System.Windows.Forms.TabPage ConnectionTab;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button usernameButton;
        private System.Windows.Forms.TextBox IPaddressBox;
        private System.Windows.Forms.TextBox usernameBox;
        private System.Windows.Forms.TextBox portBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.TabControl TabControlSettings;
    }
}

