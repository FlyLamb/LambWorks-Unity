using UnityEngine;
namespace LambWorks.Networking.Server {
    public class ServerHandle {
        [ServerHandler((int)ClientPackets.welcomeReceived)]
        public static void WelcomeReceived(int fromClient, Packet packet) {
            int clientIdCheck = packet.ReadByte();
            string username = packet.ReadString();
            Debug.Log($"{Server.clients[fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {fromClient}.");
            Server.clients[fromClient].SendIntoGame(username);
        }

        [ServerHandler((int)ClientPackets.playerMovement)]
        public static void PlayerMovement(int fromClient, Packet packet) {
            bool[] inputs = new bool[packet.ReadInt()];
            for (int i = 0; i < inputs.Length; i++) {
                inputs[i] = packet.ReadBool();
            }
            Quaternion rotation = packet.ReadQuaternion();

            Server.clients[fromClient].player.GetComponent<PlayerController>().SetInput(inputs, rotation);
        }

        [ServerHandler((int)ClientPackets.entityMessage)]
        public static void MessageEntity(int fromClient, Packet packet) {
            int id = packet.ReadInt();
            string msg = packet.ReadString();
            object obj = packet.ReadObject();
            Server.entities[id].SendMessage(msg, obj);
        }

    }
}