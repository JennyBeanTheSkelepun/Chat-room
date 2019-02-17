using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Simpler Server Program.cs

namespace SimpleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SimpleServer server = new SimpleServer("127.0.0.1", 4444);
            server.Start();
            server.Stop();
        }
    }
}
