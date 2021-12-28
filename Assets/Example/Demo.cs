using UnityEngine;

public class Demo : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            Cursor.lockState = CursorLockMode.None;
            if (LambWorks.Networking.Client.Client.instance != null) {
                LambWorks.Networking.Client.Client.instance.Disconnect();
            }

            LambWorks.Networking.Server.Server.Stop();
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
