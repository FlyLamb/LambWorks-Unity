using System.Net;
using UnityEngine;

namespace LambWorks.Networking.Client {
    public class ClientHandle : MonoBehaviour {
        public static void Welcome(Packet packet) {
            string msg = packet.ReadString();
            int myId = packet.ReadInt();

            Debug.Log($"Message from server: {msg}");
            Client.instance.myId = myId;
            ClientSend.WelcomeReceived();

            // Now that we have the client's id, connect UDP
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        }

        public static void SpawnPlayer(Packet packet) {
            int id = packet.ReadInt();
            string username = packet.ReadString();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();

            GameManager.instance.SpawnPlayer(id, username, position, rotation);
        }

        public static void PlayerPosition(Packet packet) {
            int id = packet.ReadInt();
            Vector3 position = packet.ReadVector3();

            if (GameManager.players.TryGetValue(id, out PlayerManager player)) {
                player.transform.position = position;
            }
        }

        public static void PlayerRotation(Packet packet) {
            int id = packet.ReadInt();
            Quaternion rotation = packet.ReadQuaternion();

            if (GameManager.players.TryGetValue(id, out PlayerManager player)) {
                player.transform.rotation = rotation;
            }
        }

        public static void PlayerDisconnected(Packet packet) {
            int id = packet.ReadInt();

            Destroy(GameManager.players[id].gameObject);
            GameManager.players.Remove(id);
        }
    }
}
