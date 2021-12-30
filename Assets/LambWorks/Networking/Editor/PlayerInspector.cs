using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

[CustomEditor(typeof(LambWorks.Networking.Client.PlayerManager))]
public class PlayerInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var md = ((LambWorks.Networking.Client.PlayerManager)target).metadata;

        if (md == null) {
            GUILayout.Label("No metadata");
            return;
        }

        var t = EditorJsonUtility.ToJson(md, true);

        GUILayout.Label(t);

    }


}