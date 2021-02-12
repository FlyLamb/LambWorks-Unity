using LambWorks.Networking.Client;
using UnityEngine;

public class ConnectToServer : MonoBehaviour {
    public void Start() {
        Client.instance.ConnectToServer();
    }
}
