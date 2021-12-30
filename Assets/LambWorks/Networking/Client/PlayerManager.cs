using UnityEngine;
using System.Collections.Generic;

namespace LambWorks.Networking.Client {

    [AddComponentMenu("LambWorks/Networking/Client/Player Manager")]
    public class PlayerManager : MonoBehaviour {
        public int id;
        public string username;

        protected Dictionary<string, object> metadata;

        public float lerpSpeed = 0;

        [HideInInspector]
        public Vector3 targetPosition;

        public void Initialize(int id, string username) {
            this.id = id;
            this.username = username;
        }

        public void SetMetadata(string meta, object data) {
            if (metadata.ContainsKey(meta)) {
                metadata[meta] = data;
            } else metadata.Add(meta, data);
        }

        public object GetMetadata(string meta) {
            if (metadata.TryGetValue(meta, out object data)) {
                return data;
            } else return null;
        }

        private void Update() {
            if (lerpSpeed != 0) transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
            else transform.position = targetPosition;
        }
    }
}