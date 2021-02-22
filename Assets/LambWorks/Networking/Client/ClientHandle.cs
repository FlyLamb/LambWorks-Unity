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
            if (GameManager.players.ContainsKey(id)) { //this caused an error when the server disconnected but the client did not.
                Destroy(GameManager.players[id].gameObject);
                GameManager.players.Remove(id);
            }
        }

        public static void SpawnEntity(Packet packet) {
            string model = packet.ReadString();
            uint id = (uint)packet.ReadLong();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();
            Vector3 scale = packet.ReadVector3();

            GameManager.instance.SpawnEntity(model, id, position, rotation, scale);
        }

        public static void UpdateEntity(Packet packet) {
            uint id = (uint)packet.ReadLong();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();
            Vector3 scale = packet.ReadVector3();
            object data = packet.ReadObject();

            GameManager.entities[id].UpdateEntity(position, rotation, scale, data);
        }

        public static void DestroyEntity(Packet packet) {
            uint id = (uint)packet.ReadLong();
            GameManager.instance.KillEntity(id);
        }

        public static void MessageEntity(Packet packet) {
            uint id = (uint)packet.ReadLong();
            string msg = packet.ReadString();
            object obj = packet.ReadObject();

            GameManager.entities[id].SendMessage(msg, obj);
        }
    }
}
