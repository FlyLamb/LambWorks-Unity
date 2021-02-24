using System.Collections.Generic;

namespace LambWorks.Networking.Server {
    public partial class Server {
        /// <summary>
        /// Registers all handlers. They should be defined in the ServerHandle class
        /// </summary>
        private static void RegisterHandlers() {
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
                { (int)ClientPackets.playerMovement, ServerHandle.PlayerMovement },
                { (int)ClientPackets.entityMessage, ServerHandle.MessageEntity }
            };
        }
    }
}