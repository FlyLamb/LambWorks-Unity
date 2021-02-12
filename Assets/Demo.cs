using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    private void Start() {
        if(LambWorks.Networking.Client.Client.instance!=null)
            LambWorks.Networking.Client.Client.instance.Disconnect();

        LambWorks.Networking.Server.Server.Stop();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
