using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using Newtonsoft.Json;

[CustomEditor(typeof(LambWorks.Networking.Server.PlayerManager))]
public class PlayerManagerEditorS : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var md = ((LambWorks.Networking.Server.PlayerManager)target).metadata;

        if (md == null) {
            GUILayout.Label("No metadata");
            return;
        }

        var t = JsonConvert.SerializeObject(md, Formatting.Indented);

        GUILayout.Label(t);

    }


}