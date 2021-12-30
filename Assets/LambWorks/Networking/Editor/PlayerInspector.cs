using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityEngine;

[CustomEditor(typeof(LambWorks.Networking.Client.PlayerManager))]
public class PlayerInspector : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var md = ((LambWorks.Networking.Client.PlayerManager)target).metadata;

        if (md == null) {
            GUILayout.Label("No metadata");
            return;
        }

        foreach (var w in md) {
            GUILayout.Label($"{w.Key} : {w.Key}");
        }
    }
}