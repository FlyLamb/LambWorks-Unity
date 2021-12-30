using LambWorks.Networking.Client;
using UnityEngine;

public class ConnectToServer : MonoBehaviour {
    [AddComponentMenu("LambWorks/Networking/Client/[C] Auto-Start Networking")]
    public void Start() {
        Client.instance.ConnectToServer();
    }
}
