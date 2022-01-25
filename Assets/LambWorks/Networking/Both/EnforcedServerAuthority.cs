using UnityEngine;

namespace LambWorks.Networking {
    [AddComponentMenu("LambWorks/Networking/Enforced Server Authority")]
    public class EnforcedServerAuthority : MonoBehaviour {
        private void Start() {
            if (NetInfo.IsServer) return;

            var rb = GetComponent<Rigidbody>();
            var rb2d = GetComponent<Rigidbody2D>();

            if (rb != null) rb.isKinematic = true;
            if (rb2d != null) rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}