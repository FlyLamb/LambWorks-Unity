using UnityEngine;
using System.Collections.Generic;

namespace LambWorks.Networking.Server {
    public class NetworkedAnimator : MonoBehaviour {
        public IMetadataIO metadata;

        private Dictionary<string, NetworkedAnimatorField> fields;

        public void SetBool(string n, bool value) {
            if (fields.ContainsKey(n)) {
                if ((bool)fields[n].value != value) {
                    fields[n] = new NetworkedAnimatorField(1, value);
                    metadata.IOWriteMetadata("animator", fields);
                }
            } else {
                fields.Add(n, new NetworkedAnimatorField(1, value));
                metadata.IOWriteMetadata("animator", fields);
            }
        }

        public void SetFloat(string n, float value) {
            if (fields.ContainsKey(n)) {
                if ((float)fields[n].value != value) {
                    fields[n] = new NetworkedAnimatorField(0, value);
                    metadata.IOWriteMetadata("animator", fields);
                }
            } else {
                fields.Add(n, new NetworkedAnimatorField(0, value));
                metadata.IOWriteMetadata("animator", fields);
            }
        }
    }
}