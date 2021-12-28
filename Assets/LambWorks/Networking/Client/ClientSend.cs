using UnityEngine;

namespace LambWorks.Networking.Client {
    public class ClientSend {
        /// <summary>Sends a packet to the server via TCP.</summary>
        /// <param name="packet">The packet to send to the sever.</param>
        public static void SendTCPData(Packet packet) {
            packet.WriteLength();
            Client.instance.tcp.SendData(packet);
        }

        /// <summary>Sends a packet to the server via UDP.</summary>
        /// <param name="packet">The packet to send to the sever.</param>
        public static void SendUDPData(Packet packet) {
            packet.WriteLength();
            Client.instance.udp.SendData(packet);
        }

    }
}