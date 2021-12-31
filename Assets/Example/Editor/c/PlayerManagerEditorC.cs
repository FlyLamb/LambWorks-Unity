using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Newtonsoft.Json;

[CustomEditor(typeof(LambWorks.Networking.Client.PlayerManager))]
public class PlayerManagerEditorC : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var md = ((LambWorks.Networking.Client.PlayerManager)target).metadata;

        if (md == null) {
            GUILayout.Label("No metadata");
            return;
        }

        var t = JsonConvert.SerializeObject(md, Formatting.Indented);

        GUILayout.Label(t);

    }


}