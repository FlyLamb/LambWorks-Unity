using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using LambWorks.Networking.Client;

public class WriteMeta : MonoBehaviour {
    public PlayerManager pl;
    private void OnGUI() {
        GUILayout.Label(JsonConvert.SerializeObject(pl.metadata));
    }
}
