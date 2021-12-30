namespace LambWorks.Networking.Server {

    public class ServerSend {
        /// <summary>Sends a packet to a client via TCP.</summary>
        /// <param name="toClient">The client to send the packet the packet to.</param>
        /// <param name="packet">The packet to send to the client.</param>
        public static void SendTCPData(int toClient, Packet packet) {
            packet.WriteLength();
            Server.clients[toClient].tcp.SendData(packet);
        }

        /// <summary>Sends a packet to a client via UDP.</summary>
        /// <param name="toClient">The client to send the packet the packet to.</param>
        /// <param name="packet">The packet to send to the client.</param>
        public static void SendUDPData(int toClient, Packet packet) {
            packet.WriteLength();
            Server.clients[toClient].udp.SendData(packet);
        }

        /// <summary>Sends a packet to all clients via TCP.</summary>
        /// <param name="packet">The packet to send.</param>
        public static void SendTCPDataToAll(Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                Server.clients[i].tcp.SendData(packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via TCP.</summary>
        /// <param name="exceptClient">The client to NOT send the data to.</param>
        /// <param name="packet">The packet to send.</param>
        public static void SendTCPDataToAll(int exceptClient, Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                if (i != exceptClient) {
                    Server.clients[i].tcp.SendData(packet);
                }
            }
        }

        /// <summary>Sends a packet to all clients via UDP.</summary>
        /// <param name="packet">The packet to send.</param>
        public static void SendUDPDataToAll(Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                Server.clients[i].udp.SendData(packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via UDP.</summary>
        /// <param name="exceptClient">The client to NOT send the data to.</param>
        /// <param name="packet">The packet to send.</param>
        public static void SendUDPDataToAll(int exceptClient, Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                if (i != exceptClient) {
                    Server.clients[i].udp.SendData(packet);
                }
            }
        }

        ///<summary>Sends data to all clients using the provided transport</summary>
        ///<param name="transport">The transport to use</param>
        ///<param name="except">The client to NOT send the data to</param>
        ///<param name="packet">The packet to send</param>
        public static void SendDataToAll(TransportType transport, int except, Packet packet) {
            switch (transport) {
                case TransportType.dummy:
                    return;
                case TransportType.udp:
                    SendUDPDataToAll(except, packet);
                    break;
                case TransportType.tcp:
                    SendTCPDataToAll(except, packet);
                    break;
            }
        }

        ///<summary>Sends data to all clients using the provided transport</summary>
        ///<param name="transport">The transport to use</param>
        ///<param name="packet">The packet to send</param>
        public static void SendDataToAll(TransportType transport, Packet packet) {
            switch (transport) {
                case TransportType.dummy:
                    return;
                case TransportType.udp:
                    SendUDPDataToAll(packet);
                    break;
                case TransportType.tcp:
                    SendTCPDataToAll(packet);
                    break;
            }
        }

        ///<summary>Sends data to specified client using the provided transport</summary>
        ///<param name="transport">The transport to use</param>
        ///<param name="id">The client to send the data to</param>
        ///<param name="packet">The packet to send</param>
        public static void SendData(TransportType transport, int toClient, Packet packet) {
            switch (transport) {
                case TransportType.dummy:
                    return;
                case TransportType.udp:
                    SendUDPData(toClient, packet);
                    break;
                case TransportType.tcp:
                    SendTCPData(toClient, packet);
                    break;
            }
        }

    }
}