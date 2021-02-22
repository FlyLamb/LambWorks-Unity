using System.Collections.Generic;

namespace LambWorks.Networking.Client {
    public partial class Client {
        /// <summary>
        /// Registers all handlers. They should be defined in the ClientHandle class
        /// </summary>
        private static void RegisterHandlers() {
            packetHandlers = new Dictionary<int, PacketHandler>() {
                { (int)ServerPackets.welcome, ClientHandle.Welcome },
                { (int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer },
                { (int)ServerPackets.playerPosition, ClientHandle.PlayerPosition },
                { (int)ServerPackets.playerRotation, ClientHandle.PlayerRotation },
                { (int)ServerPackets.playerDisconnected, ClientHandle.PlayerDisconnected },
                { (int)ServerPackets.entitySpawn, ClientHandle.SpawnEntity },
                { (int)ServerPackets.entityUpdate, ClientHandle.UpdateEntity },
                { (int)ServerPackets.entityDestroy, ClientHandle.DestroyEntity },
                { (int)ServerPackets.entityMessage, ClientHandle.MessageEntity }
            };
        }
    }
}