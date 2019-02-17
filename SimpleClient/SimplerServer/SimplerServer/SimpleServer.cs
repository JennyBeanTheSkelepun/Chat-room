using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Packets;

namespace SimpleServer
{
    class SimpleServer
    {
        private TcpListener tcpListener;
        private List<Client> clients;
        private BinaryFormatter formatter;

        private UdpClient udpListener;
        private IPEndPoint udpEndPoint;
        private Thread udpThread;

        private Thread tcpThread;

        private char[] XO_Grid;
        private Client XO_Player1;
        private Client XO_Player2;
        

        public SimpleServer(string ipAddress, int port)
        {
            IPAddress ip = IPAddress.Parse(ipAddress);

            tcpListener = new TcpListener(ip, port);

            clients = new List<Client>();

            formatter = new BinaryFormatter();

            udpListener = new UdpClient(port);
            udpEndPoint = new IPEndPoint(ip, port);

            XO_Grid = new char[9] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' };
        }

        public void Start()
        {
            tcpListener.Start();
            Console.WriteLine("Listener Started.");

            udpThread = new Thread(new ThreadStart(UdpListen));
            udpThread.Start();

            while (true)
            {
                Socket socket = tcpListener.AcceptSocket();
                Client client = new Client(socket);
                clients.Add(client);
                Console.WriteLine("Connection Accepted.");

                tcpThread = new Thread(new ParameterizedThreadStart(ClientMethod));
                tcpThread.Start(client);

            }
        }

        public void Stop()
        {
            tcpListener.Stop();
        }
    
        private void ClientMethod(object clientObj)
        {
            Client client = (Client)clientObj;

            //string receivedMessage = "";

            NetworkStream stream = client.GetStream();

            BinaryReader reader = client.GetReader();
            BinaryWriter writer = client.GetWriter();

            ChatMessagePacket message = new ChatMessagePacket("Server - Welcome! Set a nickname to continue.");
            client.Send(message);
            writer.Flush();


            int noOfIncomingBytes;
            while ((noOfIncomingBytes = client.GetReader().ReadInt32()) != 0)
            {
                Byte[] bytes = reader.ReadBytes(noOfIncomingBytes);
                MemoryStream memoryStream = new MemoryStream(bytes);
                Packet packet = formatter.Deserialize(memoryStream) as Packet;
                HandlePacket(client, packet);
                //writer.Flush();
                if(!client.IsConnected())
                {
                    break;
                }
            }
            
        }

        private void UdpListen()
        {
            while(true)
            {
                byte[] bytes = udpListener.Receive(ref udpEndPoint);
                MemoryStream memoryStream = new MemoryStream(bytes);
                Packet packet = formatter.Deserialize(memoryStream) as Packet;
                UdpHandlePacket(udpEndPoint, packet);
            }
        }

        private void UdpSend(IPEndPoint endpoint, Packet packet)
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, packet);
            Byte[] buffer = memoryStream.GetBuffer();
            udpListener.Send(buffer, buffer.Length, endpoint);
        }

        private void HandlePacket(Client client, Packet packet)
        {
            switch(packet.GetType())
            {
                case packetType.EMPTY:
                    break;

                case packetType.CHATMESSAGE:
                    string message = ((ChatMessagePacket)packet).message;
                    ChatMessagePacket chatResponse = new ChatMessagePacket(client.GetName() +" said: " + message);
                    Console.WriteLine("Message Recieved From " + client.GetName() + ": " + message);
                    SendToAll(chatResponse);
                    break;

                case packetType.NICKNAME:
                    string name = ((NicknamePacket)packet).nickname;
                    client.SetName(name);
                    ChatMessagePacket nicknameResponse = new ChatMessagePacket("Server - Username set to " + name);
                    client.Send(nicknameResponse);
                    Console.WriteLine("Server - Client set username to " + name);
                    nicknameResponse.message = "Server - " + name + " joined the chat! Say hello!";
                    SendToAll(nicknameResponse);

                    GamePacket gamePacket = new GamePacket("");
                    if (XO_Player1 != null)
                    {
                        gamePacket.message = "O - " + XO_Player1.GetName();
                        client.Send(packet);
                    }
                    if (XO_Player2 != null)
                    {
                        gamePacket.message = "X - " + XO_Player2.GetName();
                        client.Send(packet);
                    }

                    break;
                case packetType.GAME:
                    string command = ((GamePacket)packet).message;
                    XO_GameUpdate(client, command);
                    break;
                case packetType.NOTICE:
                    string notice = ((NoticePacket)packet).notice;
                    ChatMessagePacket broadcast = new ChatMessagePacket("Server - " + notice);
                    SendToAll(broadcast);
                    break;
                case packetType.FUNCTION:
                    string text = ((FunctionPacket)packet).command;
                    HandleCommand(text, client);
                    break;
            }
        }

        private void UdpHandlePacket(IPEndPoint endPoint, Packet packet)
        {
            switch(packet.GetType())
            {
                case packetType.EMPTY:
                    Console.WriteLine("UDP - Empty Packet Recieved.");
                    break;

                case packetType.CHATMESSAGE:

                    break;

                case packetType.GAME:
                    Console.WriteLine("UDP - Game Packet Recieved.");
                    break;

                case packetType.NICKNAME:
                    Console.WriteLine("UDP - Nickname Packet Recieved.");
                    break;

                case packetType.NOTICE:
                    Console.WriteLine("UDP - Notice Packet Recieved.");
                    string notice = ((NoticePacket)packet).notice;
                    ChatMessagePacket broadcast = new ChatMessagePacket("UDP Server - " + notice);
                    UdpSend(endPoint, broadcast);
                    break;
            }
        }

        private void SendToAll(Packet packet)
        {
            foreach (Client c in clients)
            {
                c.Send(packet);
            }
        }

        private void HandleCommand(string command, Client client)
        {
            ChatMessagePacket feedback = new ChatMessagePacket("");
            switch (command)
            {
                case "Q":
                    feedback.message = client.GetName() +  " has left the chat!";
                    SendToAll(feedback);
                    client.Close();
                    clients.Remove(client);
                    /*
                    for (int i = 0; i < clients.Count(); i++)
                    {
                        if(client == clients[i])
                        {
                            clients.Remove
                        }
                    }
                    */
                    break;

            }
        }

        /// 
        /// GAME LOGIC
        /// 

        private void XO_StartGame()
        {
            // Clears the Grid
            int pointer = 0;
            foreach (char x in XO_Grid)
            {
                XO_Grid[pointer] = ' ';
                pointer++;
            }

            GamePacket packet = new GamePacket("CLR");
            SendToAll(packet);

            // Tells player 1 its their turn and starts the game
            packet = new GamePacket("Turn");
            XO_Player1.Send(packet);
        }

        private void XO_EndGame(char winner)
        {
            GamePacket packet = new GamePacket("");
            ChatMessagePacket chatpacket = new ChatMessagePacket("");

            if (winner == 'o')
            {
                packet.message = "win";
                XO_Player1.Send(packet);
                packet.message = "lose";
                XO_Player2.Send(packet);
                chatpacket.message = "Server - " + XO_Player1.GetName() + " won a game of Naughts & Crosses against " + XO_Player2.GetName();
                SendToAll(chatpacket);
            }
            else if (winner == 'x')
            {
                packet.message = "win";
                XO_Player2.Send(packet);
                packet.message = "lose";
                XO_Player1.Send(packet);
                chatpacket.message = "Server - " + XO_Player2.GetName() + " won a game of Naughts & Crosses against " + XO_Player1.GetName();
                SendToAll(chatpacket);
            }
            else if (winner == 'd')
            {
                packet.message = "draw";
                XO_Player1.Send(packet);
                XO_Player2.Send(packet);
                chatpacket.message = "Server - " + XO_Player2.GetName() + " & " + XO_Player1.GetName() + " were tied in a game of Naughts & Crosses!";
                SendToAll(chatpacket);
            }
        }

        private void XO_GameUpdate(Client client, string command)
        {
            GamePacket packet = new GamePacket("");
            ChatMessagePacket chatpacket = new ChatMessagePacket("");

            if (XO_Player1 == null)
            {
                packet.message = "P1Q";
                SendToAll(packet);
            }
            if (XO_Player2 == null)
            {
                packet.message = "P2Q";
                SendToAll(packet);
            }


            switch (command)
            {
                case "P1J":
                    if (XO_Player1 == null)
                    {
                        XO_Player1 = client;
                        packet.message = "O - " + XO_Player1.GetName();
                        SendToAll(packet);
                        chatpacket.message = XO_Player1.GetName() + " has joined the game as Player 1!";
                        SendToAll(chatpacket);

                        if(XO_Player2 != null)
                        {
                            XO_StartGame();
                        }
                    }
                    else
                    {
                        chatpacket.message = "Server [X&O] - Someone has already taken this game slot!";
                        client.Send(chatpacket);
                        packet.message = "X - " + XO_Player1.GetName();
                        client.Send(packet);
                    }
                    break;

                case "P1Q":
                    packet.message = "P1Q";
                    SendToAll(packet);
                    chatpacket.message = XO_Player1.GetName() + " has left the game.";
                    SendToAll(chatpacket);
                    XO_Player1 = null;
                    break;

                case "P2J":
                    if (XO_Player2 == null)
                    {
                        XO_Player2 = client;
                        packet.message = "X - " + XO_Player2.GetName();
                        SendToAll(packet);
                        chatpacket.message = XO_Player2.GetName() + " has joined the game as Player 2!";
                        SendToAll(chatpacket);

                        if (XO_Player1 != null)
                        {
                            XO_StartGame();
                        }
                    }
                    else
                    {
                        chatpacket.message = "Server [X&O] - Someone has already taken this game slot!";
                        client.Send(chatpacket);
                        packet.message = "O - " + XO_Player1.GetName();
                        client.Send(packet);
                    }
                    break;

                case "P2Q":
                    packet.message = "P2Q";
                    SendToAll(packet);
                    chatpacket.message = XO_Player2.GetName() + " has left the game.";
                    SendToAll(chatpacket);
                    XO_Player2 = null;
                    break;

                case "0":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X0";
                        SendToAll(packet);
                        XO_Grid[0] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O0";
                        SendToAll(packet);
                        XO_Grid[0] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "1":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X1";
                        SendToAll(packet);
                        XO_Grid[1] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O1";
                        SendToAll(packet);
                        XO_Grid[1] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "2":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X2";
                        SendToAll(packet);
                        XO_Grid[2] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O2";
                        SendToAll(packet);
                        XO_Grid[2] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "3":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X3";
                        SendToAll(packet);
                        XO_Grid[3] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O3";
                        SendToAll(packet);
                        XO_Grid[3] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "4":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X4";
                        SendToAll(packet);
                        XO_Grid[4] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O4";
                        SendToAll(packet);
                        XO_Grid[4] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "5":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X5";
                        SendToAll(packet);
                        XO_Grid[5] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O5";
                        SendToAll(packet);
                        XO_Grid[5] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "6":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X6";
                        SendToAll(packet);
                        XO_Grid[6] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O6";
                        SendToAll(packet);
                        XO_Grid[6] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "7":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X7";
                        SendToAll(packet);
                        XO_Grid[7] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O7";
                        SendToAll(packet);
                        XO_Grid[7] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;

                case "8":
                    if (XO_Player1 == client)
                    {
                        packet.message = "X8";
                        SendToAll(packet);
                        XO_Grid[8] = 'o';
                        packet.message = "Turn";
                        XO_Player2.Send(packet);
                        XO_CheckWin();
                    }
                    else if (XO_Player2 == client)
                    {
                        packet.message = "O8";
                        SendToAll(packet);
                        XO_Grid[8] = 'x';
                        packet.message = "Turn";
                        XO_Player1.Send(packet);
                        XO_CheckWin();
                    }
                    break;
            }
        }

        private void XO_CheckWin()
        {
            if(XO_Grid[0] == XO_Grid[1] && XO_Grid[1] == XO_Grid[2])
            {
                if (XO_Grid[0] == 'x' || XO_Grid[0] == 'o')
                {
                    XO_EndGame(XO_Grid[0]);
                }
            }

            if(XO_Grid[3] == XO_Grid[4] && XO_Grid[4] == XO_Grid[5])
            {
                if (XO_Grid[3] == 'x' || XO_Grid[3] == 'o')
                {
                    XO_EndGame(XO_Grid[3]);
                }
            }

            if (XO_Grid[6] == XO_Grid[7] && XO_Grid[7] == XO_Grid[8])
            {
                if (XO_Grid[6] == 'x' || XO_Grid[6] == 'o')
                {
                    XO_EndGame(XO_Grid[6]);
                }
            }

            if (XO_Grid[0] == XO_Grid[3] && XO_Grid[3] == XO_Grid[6])
            {
                if (XO_Grid[0] == 'x' || XO_Grid[0] == 'o')
                {
                    XO_EndGame(XO_Grid[0]);
                }
            }

            if (XO_Grid[1] == XO_Grid[4] && XO_Grid[4] == XO_Grid[7])
            {
                if (XO_Grid[1] == 'x' || XO_Grid[1] == 'o')
                {
                    XO_EndGame(XO_Grid[1]);
                }
            }

            if (XO_Grid[2] == XO_Grid[5] && XO_Grid[5] == XO_Grid[8])
            {
                if (XO_Grid[2] == 'x' || XO_Grid[2] == 'o')
                {
                    XO_EndGame(XO_Grid[2]);
                }
            }

            if (XO_Grid[0] == XO_Grid[4] && XO_Grid[4] == XO_Grid[8])
            {
                if (XO_Grid[0] == 'x' || XO_Grid[0] == 'o')
                {
                    XO_EndGame(XO_Grid[0]);
                }
            }

            if (XO_Grid[2] == XO_Grid[4] && XO_Grid[4] == XO_Grid[6])
            {
                if (XO_Grid[2] == 'x' || XO_Grid[2] == 'o')
                {
                    XO_EndGame(XO_Grid[2]);
                }
            }

            int emptyCount = 0;
            foreach(char c in XO_Grid)
            {
                if (c != 'x' && c != 'o')
                {
                    emptyCount++;
                }
            }

            if (emptyCount == 0)
            {
                XO_EndGame('d');
            }
        }
    }
}
