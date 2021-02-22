//This enum should be the same both on client and on server, separating this into two files makes you able to just copy-paste this.
namespace LambWorks.Networking {
    /// <summary>Sent from server to client.</summary>
    public enum ServerPackets {
        welcome = 1,
        spawnPlayer,
        playerPosition,
        playerRotation,
        playerDisconnected,
        entityUpdate,
        entitySpawn,
        entityDestroy,
        entityMessage
    }
}