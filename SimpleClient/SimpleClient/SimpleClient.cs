using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;


namespace SimpleClient
{
    class SimpleClient
    {
        private TcpClient tcpClient;
        private NetworkStream networkStream;
        private StreamWriter writer;
        private StreamReader reader;

        public SimpleClient()
        {
            tcpClient = new TcpClient();

        }

        public bool Connect(string ipAddress, int port)
        {
            try
            {
                IPAddress ip = IPAddress.Parse(ipAddress);
                tcpClient.Connect(ip, port);
                networkStream = tcpClient.GetStream();
                reader = new StreamReader(networkStream, Encoding.UTF8);
                writer = new StreamWriter(networkStream, Encoding.UTF8);
            }
            catch(Exception e)
            {
                Console.WriteLine("{0} Exception Caught: ", e);
                return false;
            }
            return true;
        }

        public void Run()
        {
            string userInput;
            ProcessServerResponse();
            while ((userInput = Console.ReadLine()) != null)
            {
                writer.WriteLine(userInput);
                writer.Flush();
                ProcessServerResponse();
                if (userInput == "end")
                {
                    break;
                }
            }
            tcpClient.Close();
        }

        private void ProcessServerResponse()
        {
            Console.WriteLine("Server: " + reader.ReadLine());
            Console.WriteLine();
        }
    }
}
