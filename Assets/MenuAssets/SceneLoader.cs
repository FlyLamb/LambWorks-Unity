using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void Load(int index) {
        SceneManager.LoadScene(index);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            Load(0);
        }
    }
}
