using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LambWorks.Networking.Client {
    public partial class Client {
        /// <summary>
        /// Registers all handlers. They should be defined in the ServerHandle class
        /// </summary>
        private static void RegisterHandlers() {
            packetHandlers = new Dictionary<int, PacketHandler>() { 
                { (int)ServerPackets.welcome, ClientHandle.Welcome },
                { (int)ServerPackets.spawnPlayer, ClientHandle.SpawnPlayer },
                { (int)ServerPackets.playerPosition, ClientHandle.PlayerPosition },
                { (int)ServerPackets.playerRotation, ClientHandle.PlayerRotation },
                { (int)ServerPackets.playerDisconnected, ClientHandle.PlayerDisconnected }
            };
        }
    }
}