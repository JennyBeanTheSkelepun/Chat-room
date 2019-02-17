using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using Packets;

namespace ChatClient
{
    public partial class Form1 : Form
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;

        private BinaryWriter writer;
        private BinaryReader reader;
        private BinaryFormatter formatter;

        private bool connected = false;
        private string nickname;

        private int oddButtonClicks = 0;

        private Thread tcpThread;
        private delegate void UpdateChatWindowDelegate(string message);
        private UpdateChatWindowDelegate updateChatWindowDelegate;

        private delegate void GameUpdateDelegate(string message);
        private GameUpdateDelegate gameUpdateDelegate;

        private Socket udpSocket;
        private IPEndPoint udpEndPoint;
        private Thread UdpThread;

        int XO_PlayerNumber;

        public Form1()
        {
            InitializeComponent();
            
            formatter = new BinaryFormatter();

            tcpClient = new TcpClient();

            usernameButton.Enabled = false;
            sendButton.Enabled = false;
            disconnectButton.Enabled = false;
            UDP_PingButton.Enabled = false;

            XO_Player1Button.Enabled = false;
            XO_Player2Button.Enabled = false;

            XO_DisableButtons();

            udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        }

        public bool Connect(string ipAddress, int port)
        {
            try
            {
        
                IPAddress ip = IPAddress.Parse(ipAddress);
                tcpClient.Connect(ipAddress, port);

                networkStream = tcpClient.GetStream();

                reader = new BinaryReader(networkStream, Encoding.UTF8);
                writer = new BinaryWriter(networkStream, Encoding.UTF8);
                formatter = new BinaryFormatter();

                updateChatWindowDelegate = new UpdateChatWindowDelegate(UpdateChatWindow);

                gameUpdateDelegate = new GameUpdateDelegate(XO_GameUpdate);

                tcpThread = new Thread(new ThreadStart(ProcessServerResponse));
                tcpThread.Start();

                UdpThread = new Thread(new ThreadStart(UdpProcessServerResponse));
                UdpThread.Start();

                udpEndPoint = new IPEndPoint(ip, port);
                udpSocket.Connect(udpEndPoint);
            }
            catch (Exception e)
            {
                chatWindow.Text += "{0} Exception Caught: " + e + "\n";
                return false;
            }
            connected = true;
            return true;
        }
        private void Send(Packet packet)
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, packet);
            Byte[] buffer = memoryStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

        //Recieves a Message from the Server
        private void ProcessServerResponse()
        {
            int noOfIncomingBytes;
            while ((noOfIncomingBytes = reader.ReadInt32()) != 0)
            {
                Byte[] bytes = reader.ReadBytes(noOfIncomingBytes);
                MemoryStream memoryStream = new MemoryStream(bytes);
                Packet packet = formatter.Deserialize(memoryStream) as Packet;
                HandlePacket(packet);
            }
        }

        private void UdpProcessServerResponse()
        {
            int noOfIncomingBytes;
            byte[] bytes = new byte[512];

            while ((noOfIncomingBytes = udpSocket.Receive(bytes)) != 0)
            {
                MemoryStream memoryStream = new MemoryStream(bytes);
                Packet packet = formatter.Deserialize(memoryStream) as Packet;
                HandlePacket(packet);
            }
        }

        private void UdpSend(Packet packet)
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, packet);
            Byte[] buffer = memoryStream.GetBuffer();
            udpSocket.Send(buffer);
        }

        public void UpdateChatWindow(string message)
        {
            if (InvokeRequired)
            {
                Invoke(updateChatWindowDelegate, message);
            }
            else
            {
                chatWindow.Text += message + "\n";
            }
        }

        public BinaryReader GetBinaryReader()
        {
            return reader;
        }
        public BinaryWriter GetBinaryWriter()
        {
            return writer;
        }

        private void HandlePacket(Packet packet)
        {
            switch (packet.GetType())
            {
                case packetType.EMPTY:
                    break;

                case packetType.CHATMESSAGE:
                    string message = ((ChatMessagePacket)packet).message;
                    message = message + "\n";
                    UpdateChatWindow(message);
                    break;

                case packetType.GAME:
                    XO_GameUpdate(((GamePacket)packet).message);
                    break;
            }
        }


        private void ConnectButtonClick(object sender, EventArgs e)
        {
            string ip = IPaddressBox.Text;
            int port = Convert.ToInt32(this.portBox.Text);
            Connect(ip, port);
            usernameButton.Enabled = true;
            disconnectButton.Enabled = true;
        }

        private void DisconnectButtonClick(object sender, EventArgs e)
        {
            FunctionPacket packet = new FunctionPacket("Q");
            Send(packet);

            tcpThread.Abort();
            tcpClient.Close();

            UdpThread.Abort();
            udpSocket.Close();

            this.Close();
        }

        private void SendButtonClick(object sender, EventArgs e)
        {
            string message = messageBox.Text;
            ChatMessagePacket packet = new ChatMessagePacket(message);
            Send(packet);
            messageBox.Text = "";
        }

        private void UsernameButtonClick(object sender, EventArgs e)
        {
            nickname = usernameBox.Text;
            NicknamePacket name = new NicknamePacket(nickname);
            Send(name);
            writer.Flush();
            sendButton.Enabled = true;
            XO_Player1Button.Enabled = true;
            XO_Player2Button.Enabled = true;
            UDP_PingButton.Enabled = true;
        }

        private void OddButton_Click(object sender, EventArgs e)
        {
            oddButtonClicks++;
            switch (oddButtonClicks)
            {
                case 0:
                    OddButton.Text = "Don't Push Me!";
                    break;
                case 1:
                    OddButton.Text = "Ouch!";
                    break;
                case 2:
                    OddButton.Text = "Stop it!";
                    break;
                case 3:
                    OddButton.Text = "Hey!";
                    break;
                case 4:
                    OddButton.Text = "This is mean!";
                    break;
                case 5:
                    OddButton.Text = "That's Enough!";
                    break;
                case 6:
                    OddButton.Text = "Please!";
                    break;
                case 7:
                    OddButton.Text = "No more!";
                    break;
                case 8:
                    OddButton.Text = "X_X";
                    OddButton.Enabled = false;
                    if (connected)
                    {
                        NoticePacket packet = new NoticePacket(nickname + " killed their button! For shame!");
                        UdpSend(packet);
                    }
                    break;
            }
        }

        private void UDP_PingButton_Click(object sender, EventArgs e)
        {
            NoticePacket ping = new NoticePacket(nickname + " sent a ping, then recieved a pong!");
            UdpSend(ping);
        }

        /// GAME FUNCTIONALITY ///

        private void XO_GameStart()
        {
            XO_Clear();
            XO_EnableButtons();
        }

        private void XO_GameEnd()
        {
            XO_Player1Button.Text = "X - [Empty]";
            XO_Player1Button.Enabled = true;
            XO_Player2Button.Text = "O - [Empty]";
            XO_Player2Button.Enabled = true;
            XO_PlayerNumber = 0;
        }

        private void XO_GameUpdate(string message)
        {
            if (InvokeRequired)
            {
                Invoke(gameUpdateDelegate, message);
            }
            else
            {
                switch (message)
                {
                    case "P1Q":
                        XO_Player1Button.Text = "O - [Empty]";
                        XO_Player1Button.Enabled = true;
                        break;

                    case "P2Q":
                        XO_Player2Button.Text = "X - [Empty]";
                        XO_Player2Button.Enabled = true;
                        break;

                    case "Turn":
                        XO_EnableButtons();
                        XO_OutputBox.Text = "Your Turn";
                        break;

                    case "win":
                        XO_OutputBox.Text = "You won!";
                        XO_Exit();
                        break;

                    case "lose":
                        XO_OutputBox.Text = "You lost!";
                        XO_Exit();
                        break;

                    case "draw":
                        XO_OutputBox.Text = "Its a draw!";
                        XO_Exit();
                        break;

                    case "CLR":
                        XO_Clear();
                        break;

                    case "X0":
                        XO_0.Text = "X";
                        break;

                    case "X1":
                        XO_1.Text = "X";
                        break;

                    case "X2":
                        XO_2.Text = "X";
                        break;

                    case "X3":
                        XO_3.Text = "X";
                        break;

                    case "X4":
                        XO_4.Text = "X";
                        break;

                    case "X5":
                        XO_5.Text = "X";
                        break;

                    case "X6":
                        XO_6.Text = "X";
                        break;

                    case "X7":
                        XO_7.Text = "X";
                        break;

                    case "X8":
                        XO_8.Text = "X";
                        break;

                    case "O0":
                        XO_0.Text = "O";
                        break;

                    case "O1":
                        XO_1.Text = "O";
                        break;

                    case "O2":
                        XO_2.Text = "O";
                        break;

                    case "O3":
                        XO_3.Text = "O";
                        break;

                    case "O4":
                        XO_4.Text = "O";
                        break;

                    case "O5":
                        XO_5.Text = "O";
                        break;

                    case "O6":
                        XO_6.Text = "O";
                        break;

                    case "O7":
                        XO_7.Text = "O";
                        break;

                    case "O8":
                        XO_8.Text = "O";
                        break;

                    default:
                        if(message[0] == 'O')
                        {
                            XO_Player1Button.Text = message;
                            XO_Player1Button.Enabled = false;
                        }
                        else if (message[0] == 'X')
                        {
                            XO_Player2Button.Text = message;
                            XO_Player2Button.Enabled = false;
                        }
                        else
                        {
                            XO_OutputBox.Text = message;
                        }
                        break;
                }
            }
        }

        private void XO_Clear()
        {
            XO_0.Text = "";
            XO_1.Text = "";
            XO_2.Text = "";
            XO_3.Text = "";
            XO_4.Text = "";
            XO_5.Text = "";
            XO_6.Text = "";
            XO_7.Text = "";
            XO_8.Text = "";
        }

        private void XO_EnableButtons()
        {
            if(XO_0.Text == "")
                XO_0.Enabled = true;

            if (XO_1.Text == "")
                XO_1.Enabled = true;

            if (XO_2.Text == "")
                XO_2.Enabled = true;

            if (XO_3.Text == "")
                XO_3.Enabled = true;

            if (XO_4.Text == "")
                XO_4.Enabled = true;

            if (XO_5.Text == "")
                XO_5.Enabled = true;

            if (XO_6.Text == "")
                XO_6.Enabled = true;

            if (XO_7.Text == "")
                XO_7.Enabled = true;

            if (XO_8.Text == "")
                XO_8.Enabled = true;
        }

        private void XO_DisableButtons()
        {
            XO_0.Enabled = false;
            XO_1.Enabled = false;
            XO_2.Enabled = false;
            XO_3.Enabled = false;
            XO_4.Enabled = false;
            XO_5.Enabled = false;
            XO_6.Enabled = false;
            XO_7.Enabled = false;
            XO_8.Enabled = false;
        }

        private void XO_Player1Button_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber != 2)
            {
                XO_Player1Button.Text = "O - " + nickname;
                XO_Player1Button.Enabled = false;
                XO_Player2Button.Enabled = false;
                XO_PlayerNumber = 1;

                GamePacket gamePacket = new GamePacket("P1J");
                Send(gamePacket);
            }
        }

        private void XO_Player2Button_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber != 1)
            {
                XO_Player2Button.Text = "X - " + nickname;
                XO_Player1Button.Enabled = false;
                XO_Player2Button.Enabled = false;
                XO_PlayerNumber = 2;

                GamePacket gamePacket = new GamePacket("P2J");
                Send(gamePacket);
            }
        }

        private void XO_ExitButton_Click(object sender, EventArgs e)
        {
            XO_Exit();
        }

        private void XO_Exit()
        {
            if (XO_PlayerNumber == 1)
            {
                XO_Player1Button.Text = "O - [Empty]";
                XO_Player1Button.Enabled = true;
                XO_PlayerNumber = 0;

                GamePacket gamePacket = new GamePacket("P1Q");
                Send(gamePacket);
            }
            else if (XO_PlayerNumber == 2)
            {
                XO_Player2Button.Text = "X - [Empty]";
                XO_Player2Button.Enabled = true;
                XO_PlayerNumber = 0;

                GamePacket gamePacket = new GamePacket("P2Q");
                Send(gamePacket);
            }
        }

        private void XO_0_Click(object sender, EventArgs e)
        {
            if(XO_PlayerNumber == 1)
            {
                XO_0.Text = "O";
                
            }
            else if (XO_PlayerNumber == 2)
            {
                XO_0.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("0");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }


        private void XO_1_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_1.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_1.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("1");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }

        private void XO_2_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_2.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_2.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("2");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }

        private void XO_3_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_3.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_3.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("3");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }

        private void XO_4_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_4.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_4.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("4");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }

        private void XO_5_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_5.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_5.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("5");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }

        private void XO_6_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_6.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_6.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("6");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }

        private void XO_7_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_7.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_7.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("7");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }

        private void XO_8_Click(object sender, EventArgs e)
        {
            if (XO_PlayerNumber == 1)
            {
                XO_8.Text = "O";

            }
            else if (XO_PlayerNumber == 2)
            {
                XO_8.Text = "X";
            }

            GamePacket gamePacket = new GamePacket("8");
            Send(gamePacket);

            XO_DisableButtons();
            XO_OutputBox.Text = "Their Turn";
        }
    }
}