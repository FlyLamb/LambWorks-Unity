using UnityEngine;
using System.Collections.Generic;

namespace LambWorks.Networking.Server {
    [AddComponentMenu("LambWorks/Networking/Server/[S] Networked Animator")]
    public class NetworkedAnimator : MonoBehaviour {
        [SerializeField]
        public Component metadata;
        private IMetadataIO _metadata;

        private Dictionary<string, NetworkedAnimatorField> fields;


        private void Start() {
            _metadata = metadata as IMetadataIO;
            fields = new Dictionary<string, NetworkedAnimatorField>();
        }

        public void SetBool(string n, bool value, TransportType transportType = TransportType.udp) {
            if (fields.ContainsKey(n)) {
                if ((bool)fields[n].value != value) {
                    fields[n] = new NetworkedAnimatorField(1, value);
                    _metadata.IOWriteMetadata("animator", fields, transportType);
                }
            } else {
                fields.Add(n, new NetworkedAnimatorField(1, value));
                _metadata.IOWriteMetadata("animator", fields, transportType);
            }
        }

        public void SetFloat(string n, float value, TransportType transportType = TransportType.udp) {
            if (fields.ContainsKey(n)) {
                if ((float)fields[n].value != value) {
                    fields[n] = new NetworkedAnimatorField(0, value);
                    _metadata.IOWriteMetadata("animator", fields, transportType);
                }
            } else {
                fields.Add(n, new NetworkedAnimatorField(0, value));
                _metadata.IOWriteMetadata("animator", fields, transportType);
            }
        }
    }
}