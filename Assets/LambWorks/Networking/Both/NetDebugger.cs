using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LambWorks.Networking {
    [AddComponentMenu("LambWorks/Networking/Network Debug Info")]
    public class NetDebugger : MonoBehaviour {

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
            GUILayout.Space(10);
            GUILayout.Label($"FPS {(1f / Time.deltaTime):0.00}");

            GUILayout.Space(20);
            GUILayout.Label($"TCP ↑ {c_tcp_up} b/s");
            GUILayout.Label($"UDP ↑ {c_udp_up} b/s");

            GUILayout.Label($"UDP ↓ {c_udp_down} b/s");
            GUILayout.Label($"TCP ↓ {c_tcp_down} b/s");

            GUILayout.Space(20);


            GUILayout.Label($"TOTAL TRANSFER {(NetInfo.downloadedTcp + NetInfo.downloadedUdp + NetInfo.uploadedTcp + NetInfo.uploadedUdp) / 1000f:0.00} kb");
        }
    }

}