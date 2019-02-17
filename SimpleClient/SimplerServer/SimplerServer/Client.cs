using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Packets;

namespace SimpleServer
{
    class Client
    {
        private Socket socket;
        private NetworkStream stream;

        private BinaryReader reader;
        private BinaryWriter writer;

        private BinaryFormatter formatter;

        private string nickName;


        public Client(Socket s)
        {
            socket = s;
            stream = new NetworkStream(socket);
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
            formatter = new BinaryFormatter();
            nickName = "[NONAME]";

        }
        public NetworkStream GetStream()
        {
            return stream;
        }
        public BinaryReader GetReader()
        {
            return reader;
        }
        public BinaryWriter GetWriter()
        {
            return writer;
        }
        public Socket GetSocket()
        {
            return socket;
        }

        public string GetName()
        {
            return nickName;
        }

        public void SetName(string newName)
        {
            nickName = newName;
        }
        
        public void Close()
        {
            stream.Close();
            reader.Close();
            writer.Close();
            socket.Close();
            socket.Dispose();
        }

        public void Send(Packet packet)
        {
            MemoryStream memoryStream = new MemoryStream();
            formatter.Serialize(memoryStream, packet);
            Byte[] buffer = memoryStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

        public bool IsConnected()
        {
            try
            {
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException) { return false; }
        }
    }
}
