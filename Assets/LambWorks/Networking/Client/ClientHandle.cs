using System.Net;
using UnityEngine;

namespace LambWorks.Networking.Client {

    public class ClientHandle {
        [ClientHandler((int)ServerPackets.welcome)]
        public static void Welcome(Packet packet) {
            string msg = packet.ReadString();
            int myId = packet.ReadInt();

            Debug.Log($"Message from server: {msg}");
            Client.instance.myId = myId;
            SendMethods.WelcomeReceived();

            // Now that we have the client's id, connect UDP
            Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
        }

        [ClientHandler((int)ServerPackets.spawnPlayer)]
        public static void SpawnPlayer(Packet packet) {
            int id = packet.ReadInt();
            string username = packet.ReadString();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();

            GameManager.instance.SpawnPlayer(id, username, position, rotation);
        }

        [ClientHandler((int)ServerPackets.playerPosition)]
        public static void PlayerPosition(Packet packet) {
            int id = packet.ReadInt();
            Vector3 position = packet.ReadVector3();

            if (GameManager.players.TryGetValue(id, out PlayerManager player)) {
                player.targetPosition = position;
            }
        }

        [ClientHandler((int)ServerPackets.playerRotation)]
        public static void PlayerRotation(Packet packet) {
            int id = packet.ReadInt();
            Quaternion rotation = packet.ReadQuaternion();

            if (GameManager.players.TryGetValue(id, out PlayerManager player)) {
                player.transform.rotation = rotation;
            }
        }

        [ClientHandler((int)ServerPackets.playerDisconnected)]
        public static void PlayerDisconnected(Packet packet) {
            int id = packet.ReadInt();
            if (id == Client.instance.myId) {
                Client.instance.Disconnect();
                UnityEngine.SceneManagement.SceneManager.LoadScene(0); // if we were disconnected, go back to the menu.
            }
            if (GameManager.players.ContainsKey(id)) { //this caused an error when the server disconnected but the client did not.
                GameObject.Destroy(GameManager.players[id].gameObject);
                GameManager.players.Remove(id);
            }
        }

        [ClientHandler((int)ServerPackets.entitySpawn)]
        public static void SpawnEntity(Packet packet) {
            string model = packet.ReadString();
            uint id = (uint)packet.ReadLong();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();
            Vector3 scale = packet.ReadVector3();

            GameManager.instance.SpawnEntity(model, id, position, rotation, scale);
        }

        [ClientHandler((int)ServerPackets.entityUpdate)]
        public static void UpdateEntity(Packet packet) {
            uint id = (uint)packet.ReadLong();
            Vector3 position = packet.ReadVector3();
            Quaternion rotation = packet.ReadQuaternion();
            Vector3 scale = packet.ReadVector3();
            object data = packet.ReadObject();

            GameManager.entities[id].UpdateEntity(position, rotation, scale, data);
        }

        [ClientHandler((int)ServerPackets.entityDestroy)]
        public static void DestroyEntity(Packet packet) {
            uint id = (uint)packet.ReadLong();
            GameManager.instance.KillEntity(id);
        }

        [ClientHandler((int)ServerPackets.entityMessage)]
        public static void MessageEntity(Packet packet) {
            uint id = (uint)packet.ReadLong();
            string msg = packet.ReadString();
            object obj = packet.ReadObject();

            GameManager.entities[id].SendMessage(msg, obj);
        }
    }
}
