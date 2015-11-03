using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;

namespace MultiNet
{
    class BaseServer
    {
        Socket server = null;
        int maxPlayers, connectionPort;

        public BaseServer()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Run(int connectionPort, int maxPlayers)
        {
            this.connectionPort = connectionPort;
            this.maxPlayers = maxPlayers;
            server.Bind(new IPEndPoint(IPAddress.Any, connectionPort));
            server.Listen(Math.Abs(maxPlayers)/ 10 + 1);
            server.BeginAccept(new AsyncCallback(OnConnect), null);
        }

        private void OnConnect(IAsyncResult iar)
        {
            Socket client = server.EndAccept(iar);
            server.BeginAccept(new AsyncCallback(OnConnect), null);
        }
    }
}
