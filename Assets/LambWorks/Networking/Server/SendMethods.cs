using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LambWorks.Networking.Server {
    public partial class ServerSend {
        /// <summary>Sends a welcome message to the given client.</summary>
        /// <param name="toClient">The client to send the packet to.</param>
        /// <param name="msg">The message to send.</param>
        public static void Welcome(int toClient, string msg) {
            using (Packet packet = new Packet((int)ServerPackets.welcome)) {
                packet.Write(msg);
                packet.Write(toClient);

                SendTCPData(toClient, packet);
            }
        }

        /// <summary>Tells a client to spawn a player.</summary>
        /// <param name="toClient">The client that should spawn the player.</param>
        /// <param name="player">The player to spawn.</param>
        public static void SpawnPlayer(int toClient, Player player) {
            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer)) {
                packet.Write(player.id);
                packet.Write(player.username);
                packet.Write(player.transform.position);
                packet.Write(player.transform.rotation);

                SendTCPData(toClient, packet);
            }
        }

        /// <summary>Sends a player's updated position to all clients.</summary>
        /// <param name="player">The player whose position to update.</param>
        public static void PlayerPosition(Player player) {
            using (Packet packet = new Packet((int)ServerPackets.playerPosition)) {
                packet.Write(player.id);
                packet.Write(player.transform.position);

                SendUDPDataToAll(packet);
            }
        }

        /// <summary>Sends a player's updated rotation to all clients except to himself (to avoid overwriting the local player's rotation).</summary>
        /// <param name="player">The player whose rotation to update.</param>
        public static void PlayerRotation(Player player) {
            using (Packet packet = new Packet((int)ServerPackets.playerRotation)) {
                packet.Write(player.id);
                packet.Write(player.transform.rotation);

                SendUDPDataToAll(player.id, packet);
            }
        }

        /// <summary>Sends a player disconnected, so that the player is removed in the Clients.</summary>
        /// <param name="playerId">The player which disconnected</param>
        public static void PlayerDisconnected(int playerId) {
            using (Packet packet = new Packet((int)ServerPackets.playerDisconnected)) {
                packet.Write(playerId);

                SendTCPDataToAll(packet);
            }
        }

        /// <summary>Sends an entity to the client, if client does not have this entity than it is automatically spawned</summary>
        /// <param name="entity">The entity to send.</param>
        public static void UpdateEntity(Entity entity) {
            using (Packet packet = new Packet((int)ServerPackets.entityUpdate)) {
                packet.Write(entity.id);
                packet.Write(entity.transform.position);
                packet.Write(entity.transform.rotation);
                packet.Write(entity.transform.localScale);

                SendUDPDataToAll(packet);
            }
        }

        public static void SpawnEntity(Entity entity, int toClient = -1) {
            using (Packet packet = new Packet((int)ServerPackets.entitySpawn)) {
                packet.Write(entity.model);
                packet.Write(entity.id);
                packet.Write(entity.transform.position);
                packet.Write(entity.transform.rotation);
                packet.Write(entity.transform.localScale);

                if (toClient == -1)
                    SendTCPDataToAll(packet);
                else
                    SendTCPData(toClient, packet);
            }
        }

        public static void DestroyEntity(Entity entity) {
            using (Packet packet = new Packet((int)ServerPackets.entityDestroy)) {
                packet.Write(entity.id);

                SendTCPDataToAll(packet);
            }
        }
    }
}