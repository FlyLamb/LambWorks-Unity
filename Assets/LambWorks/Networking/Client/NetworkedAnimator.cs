using UnityEngine;
using System.Collections.Generic;
using System;

namespace LambWorks.Networking.Client {
    [AddComponentMenu("LambWorks/Networking/Server/[C] Networked Animator")]
    public class NetworkedAnimator : MonoBehaviour {
        [SerializeField]
        public Component metadata;
        private IMetadataIO _metadata;

        private Dictionary<string, NetworkedAnimatorField> fields;
        public Animator animator;

        private void Awake() {
            _metadata = metadata as IMetadataIO;
            _metadata.IOGotData += OnGotData;
        }

        private void OnGotData() {
            fields = _metadata.IOReadMetadata("animator") as Dictionary<string, NetworkedAnimatorField>;
            if (fields == null) return;
            foreach (var kv in fields) {
                switch (kv.Value.type) {
                    case 0:
                        animator.SetFloat(kv.Key, (float)kv.Value.value);
                        break;
                    case 1:
                        animator.SetBool(kv.Key, (bool)kv.Value.value);
                        break;
                }
            }
        }
    }
}