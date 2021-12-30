using UnityEngine;
using System.Collections.Generic;
using System;

namespace LambWorks.Networking.Client {
    public class NetworkedAnimator : MonoBehaviour {
        public IMetadataIO metadata;

        private Dictionary<string, NetworkedAnimatorField> fields;
        public Animator animator;

        private void Start() {
            metadata.IOGotData += OnGotData;
        }

        private void OnGotData() {
            print("got data");
            fields = metadata.IOReadMetadata("anim") as Dictionary<string, NetworkedAnimatorField>;

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