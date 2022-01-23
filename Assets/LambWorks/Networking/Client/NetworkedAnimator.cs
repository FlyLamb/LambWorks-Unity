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

        private Dictionary<string, short> triggers = new Dictionary<string, short>();

        private void Awake() {
            _metadata = metadata as IMetadataIO;
            _metadata.IOGotData += OnGotData;
        }

        private void OnGotData() {
            fields = _metadata.IOReadMetadata("anim") as NetDictionaryString;
            if (fields == null) return;
            foreach (var kv in fields) {
                var val = kv.Value;
                switch (val) {
                    case int i:
                        animator.SetInteger(kv.Key, i);
                        break;
                    case float f:
                        animator.SetFloat(kv.Key, f);
                        break;
                    case bool b:
                        animator.SetBool(kv.Key, b);
                        break;
                    case short s:
                        if (triggers.ContainsKey(kv.Key)) {
                            if (triggers[kv.Key] != s)
                                animator.SetTrigger(kv.Key);
                            triggers[kv.Key] = s;
                        } else {
                            triggers.Add(kv.Key, s);
                            animator.SetTrigger(kv.Key);
                        }
                        break;
                }
            }
        }
    }
}