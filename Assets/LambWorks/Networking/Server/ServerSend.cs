namespace LambWorks.Networking.Server {

    public partial class ServerSend {
        /// <summary>Sends a packet to a client via TCP.</summary>
        /// <param name="toClient">The client to send the packet the packet to.</param>
        /// <param name="packet">The packet to send to the client.</param>
        private static void SendTCPData(int toClient, Packet packet) {
            packet.WriteLength();
            Server.clients[toClient].tcp.SendData(packet);
        }

        /// <summary>Sends a packet to a client via UDP.</summary>
        /// <param name="toClient">The client to send the packet the packet to.</param>
        /// <param name="packet">The packet to send to the client.</param>
        private static void SendUDPData(int toClient, Packet packet) {
            packet.WriteLength();
            Server.clients[toClient].udp.SendData(packet);
        }

        /// <summary>Sends a packet to all clients via TCP.</summary>
        /// <param name="packet">The packet to send.</param>
        private static void SendTCPDataToAll(Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                Server.clients[i].tcp.SendData(packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via TCP.</summary>
        /// <param name="exceptClient">The client to NOT send the data to.</param>
        /// <param name="packet">The packet to send.</param>
        private static void SendTCPDataToAll(int exceptClient, Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                if (i != exceptClient) {
                    Server.clients[i].tcp.SendData(packet);
                }
            }
        }

        /// <summary>Sends a packet to all clients via UDP.</summary>
        /// <param name="packet">The packet to send.</param>
        private static void SendUDPDataToAll(Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                Server.clients[i].udp.SendData(packet);
            }
        }
        /// <summary>Sends a packet to all clients except one via UDP.</summary>
        /// <param name="exceptClient">The client to NOT send the data to.</param>
        /// <param name="packet">The packet to send.</param>
        private static void SendUDPDataToAll(int exceptClient, Packet packet) {
            packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++) {
                if (i != exceptClient) {
                    Server.clients[i].udp.SendData(packet);
                }
            }
        }

    }
}