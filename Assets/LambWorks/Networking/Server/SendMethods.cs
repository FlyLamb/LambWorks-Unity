using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace LambWorks.Networking.Server {
    public class SendMethods {
        /// <summary>Sends a welcome message to the given client.</summary>
        /// <param name="toClient">The client to send the packet to.</param>
        /// <param name="msg">The message to send.</param>
        public static void Welcome(int toClient, string msg) {
            using (Packet packet = new Packet((int)ServerPackets.welcome)) {
                packet.Write(msg);
                packet.Write(toClient);

                ServerSend.SendTCPData(toClient, packet);
            }
        }

        /// <summary>Tells a client to spawn a player.</summary>
        /// <param name="toClient">The client that should spawn the player.</param>
        /// <param name="player">The player to spawn.</param>
        public static void SpawnPlayer(int toClient, PlayerManager player) {
            NetworkManager.instance.OnPlayerJoin(player.id);
            using (Packet packet = new Packet((int)ServerPackets.spawnPlayer)) {
                packet.Write((short)player.id);
                packet.Write(player.username);
                packet.Write(player.transform.position);
                packet.Write(player.transform.rotation);

                ServerSend.SendTCPData(toClient, packet);
            }
        }

        /// <summary>Sends a player's updated position to all clients.</summary>
        /// <param name="player">The player whose position to update.</param>
        public static void PlayerPosition(PlayerManager player) {
            using (Packet packet = new Packet((int)ServerPackets.playerPosition)) {
                packet.Write(player.id);
                packet.Write(player.transform.position);

                ServerSend.SendUDPDataToAll(packet);
            }
        }

        /// <summary>Sends a player's updated rotation to all clients except to himself (to avoid overwriting the local player's rotation).</summary>
        /// <param name="player">The player whose rotation to update.</param>
        public static void PlayerRotation(PlayerManager player) {
            using (Packet packet = new Packet((int)ServerPackets.playerRotation)) {
                packet.Write(player.id);
                packet.Write(player.transform.rotation);

                ServerSend.SendUDPDataToAll(player.id, packet);
            }
        }

        /// <summary>Sends a player disconnected, so that the player is removed in the Clients.</summary>
        /// <param name="playerId">The player which disconnected</param>
        public static void PlayerDisconnected(byte playerId) {
            using (Packet packet = new Packet((int)ServerPackets.playerDisconnected)) {
                packet.Write(playerId);

                ServerSend.SendTCPDataToAll(packet);
            }
        }

        public static void PlayerMetadata(byte playerId, string meta, object data, TransportType transport) {
            using (Packet packet = new Packet((int)ServerPackets.playerMetadata)) {
                packet.Write(playerId);
                packet.Write(meta);
                packet.WriteObject(data);

                ServerSend.SendDataToAll(transport, packet);
            }
        }

        /// <summary>Sends an entity update to the client</summary>
        /// <param name="entity">The entity to send.</param>
        public static void UpdateEntity(Entity entity) {
            using (Packet packet = new Packet((int)ServerPackets.entityUpdate)) {
                packet.Write(entity.id);
                packet.Write(entity.origin.position);
                packet.Write(entity.origin.rotation);
                packet.Write(entity.origin.localScale);

                ServerSend.SendUDPDataToAll(packet);
            }
        }

        /// <summary>Sends an entity spawn to the client</summary>
        /// <param name="entity">The entity to send.</param>
        /// <param name="toClient">The client to which to send this, if -1 then it is all</param>
        public static void SpawnEntity(Entity entity, int toClient = -1) {
            using (Packet packet = new Packet((int)ServerPackets.entitySpawn)) {
                packet.Write(entity.model);
                packet.Write(entity.id);
                packet.Write(entity.transform.position);
                packet.Write(entity.transform.rotation);
                packet.Write(entity.transform.localScale);

                if (toClient == -1)
                    ServerSend.SendTCPDataToAll(packet);
                else
                    ServerSend.SendTCPData(toClient, packet);
            }
        }

        ///<summary>Sends an entity destroy to the client.</summary>
        /// <param name="entity">The entity to destroy.</param>
        public static void DestroyEntity(Entity entity) {
            using (Packet packet = new Packet((int)ServerPackets.entityDestroy)) {
                packet.Write(entity.id);

                ServerSend.SendTCPDataToAll(packet);
            }
        }

        ///<summary>Sends a message (essentially attempts to call a function) in the client entity</summary>
        /// <param name="entity">The entity to message.</param>
        /// <param name="message">The name of the function to call</param>
        /// <param name="parameter">The object to send as the parameter to the function</param>
        public static void MessageEntity(Entity entity, string message, object parameter = null) {
            using (Packet packet = new Packet((int)ServerPackets.entityMessage)) {
                packet.Write(entity.id);
                packet.Write(message);
                packet.WriteObject(parameter);

                ServerSend.SendTCPDataToAll(packet);
            }
        }

        public static void MetadataEntity(Entity entity, string meta, object data, TransportType transport) {
            using (Packet packet = new Packet((int)ServerPackets.entityMetadata)) {
                packet.Write(entity.id);
                packet.Write(meta);
                packet.WriteObject(data);

                ServerSend.SendDataToAll(transport, packet);
            }
        }
    }
}