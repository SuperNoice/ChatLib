using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Globalization;

namespace Chat
{
    namespace BasicElements
    {
        [Serializable]
        public struct Message
        {
            public string SystemMessage { set; get; }
            public string ClientMessage { set; get; }
        }

        public enum ServerConfig
        {
            Master, Slave, Solo
        }

        public class User
        {
            public Socket UserSocket { get; }
            public uint UserId { get; }
            public List<uint> FriendsIdList = new List<uint>();

            public User(string Adress, int Port, uint userId, AddressFamily addressFamily = AddressFamily.InterNetwork, SocketType socketType = SocketType.Stream, ProtocolType protocolType = ProtocolType.Tcp)
            {
                UserSocket = new Socket(addressFamily, socketType, protocolType);
                UserSocket.Bind(new IPEndPoint(IPAddress.Parse(Adress), Port));

                UserId = userId;
            }

            public User(Socket socket, uint userId)
            {
                UserSocket = socket;
                if (!UserSocket.IsBound) throw new Exception("Сокет не привязан");

                UserId = userId;
            }

            public string getUserAdress()
            {
                return UserSocket.RemoteEndPoint.ToString();
            }

        }

        public class Room
        {
            public int RoomId;
            public List<User> Users = new List<User>();

            public Room(int roomId)
            {
                RoomId = roomId;
            }

            public void AddUser(User user)
            {
                if (!Users.Contains(user))
                    Users.Add(user);
            }

            public void DelUser(User user)
            {
                if (Users.Contains(user))
                    Users.Remove(user);
            }
        }
    }
}