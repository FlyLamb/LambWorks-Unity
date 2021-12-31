using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LambWorks.Networking;

public class FramerateDisplay : MonoBehaviour {

    long p_tcp_up, p_tcp_down, p_udp_up, p_udp_down;
    long c_tcp_up, c_tcp_down, c_udp_up, c_udp_down;
    private IEnumerator RefreshData() {
        c_tcp_up = NetInfo.uploadedTcp - p_tcp_up;
        p_tcp_up = NetInfo.uploadedTcp;

        c_tcp_down = NetInfo.downloadedTcp - p_tcp_down;
        p_tcp_down = NetInfo.downloadedTcp;

        c_udp_up = NetInfo.uploadedUdp - p_udp_up;
        p_udp_up = NetInfo.uploadedUdp;

        c_udp_down = NetInfo.downloadedUdp - p_udp_down;
        p_udp_down = NetInfo.downloadedUdp;

        yield return new WaitForSeconds(1);
        StartCoroutine(RefreshData());
    }

    private void Start() {
        StartCoroutine(RefreshData());

    }

    private void OnGUI() {
        GUILayout.Space(140);
        GUILayout.Label("FPS " + (1f / Time.deltaTime));

        GUILayout.Space(240);
        GUILayout.Label("TCP UP " + c_tcp_up);
        GUILayout.Label("TCP DW " + c_tcp_down);
        GUILayout.Label("UDP UP " + c_udp_up);
        GUILayout.Label("UDP DW " + c_udp_down);
    }
}
