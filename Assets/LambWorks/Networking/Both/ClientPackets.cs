//This enum should be the same both on client and on server, separating this into two files makes you able to just copy-paste this.
namespace LambWorks.Networking {
    /// <summary>Sent from client to server.</summary>
    public enum ClientPackets {
        welcomeReceived = 1,
        playerMovement,
        entityMessage
    }
}
