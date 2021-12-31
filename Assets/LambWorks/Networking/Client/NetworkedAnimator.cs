using UnityEngine;
using System.Collections.Generic;
using System;
using LambWorks.Networking.Types;

namespace LambWorks.Networking.Client {
    [AddComponentMenu("LambWorks/Networking/Server/[C] Networked Animator")]
    public class NetworkedAnimator : MonoBehaviour {
        [SerializeField]
        public Component metadata;
        private IMetadataIO _metadata;

        private NetDictionaryString fields;
        public Animator animator;

        private void Awake() {
            _metadata = metadata as IMetadataIO;
            _metadata.IOGotData += OnGotData;
        }

        private void OnGotData() {
            fields = _metadata.IOReadMetadata("animator") as NetDictionaryString;
            if (fields == null) return;
            foreach (var kv in fields) {
                var val = kv.Value as NetworkedAnimatorField;
                switch (val.type) {
                    case 0:
                        animator.SetFloat(kv.Key, (float)val.value);
                        break;
                    case 1:
                        animator.SetBool(kv.Key, (bool)val.value);
                        break;
                }
            }
        }
    }
}