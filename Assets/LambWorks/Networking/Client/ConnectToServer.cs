using LambWorks.Networking.Client;
using UnityEngine;

public class ConnectToServer : MonoBehaviour {
    [AddComponentMenu("LambWorks/Networking/Client/Auto-Start Networking")]
    public void Start() {
        Client.instance.ConnectToServer();
    }
}
