using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;
using Chat.BasicElements;

namespace Chat
{
    namespace Server
    {
        public class ChatServer
        {
            public Socket ServerSocket { get; }
            public List<User> UsersList = new List<User>();

            public ChatServer(string Adress, int Port, AddressFamily addressFamily = AddressFamily.InterNetwork, SocketType socketType = SocketType.Stream, ProtocolType protocolType = ProtocolType.Tcp)
            {
                ServerSocket = new Socket(addressFamily, socketType, protocolType);
                ServerSocket.Bind(new IPEndPoint(IPAddress.Parse(Adress), Port));
            }

            public ChatServer(Socket socket)
            {
                ServerSocket = socket;
                if (!ServerSocket.IsBound) throw new Exception("Сокет не привязан");
            }

            public void Run(ServerConfig serverConfig)
            {
                if (serverConfig == ServerConfig.Master)
                {

                }

                if (serverConfig == ServerConfig.Slave)
                {

                }

                if (serverConfig == ServerConfig.Solo)
                {

                }
            }

            public void SendMesssage(Message message, User user)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream messageStream = new MemoryStream();

                formatter.Serialize(messageStream, message);

                user.UserSocket.Send(messageStream.ToArray());
            }

            public async void SendMesssageAsync(Message message, User user)
            {
                await Task.Run(() => SendMesssage(message, user));
            }

            public Message ReceiveMessage()
            {
                byte[] buffer = new byte[2048];
                int receiveLength = ServerSocket.Receive(buffer);

                BinaryFormatter formatter = new BinaryFormatter();
                MemoryStream messageStream = new MemoryStream();
                messageStream.Write(buffer, 0, receiveLength);

                return (Message)formatter.Deserialize(messageStream);
            }


        }
    }
}
