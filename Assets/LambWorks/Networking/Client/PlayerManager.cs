using UnityEngine;

namespace LambWorks.Networking.Client {

    [AddComponentMenu("LambWorks/Networking/Client/Player Manager")]
    public class PlayerManager : MonoBehaviour {
        public int id;
        public string username;

        public float lerpSpeed = 0;

        [HideInInspector]
        public Vector3 targetPosition;

        public void Initialize(int id, string username) {
            this.id = id;
            this.username = username;
        }

        private void Update() {
            if (lerpSpeed != 0) transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            else transform.position = targetPosition;
        }
    }
}